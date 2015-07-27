﻿using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace GitMerge
{
    public class MergeManagerPrefab : MergeManager
    {
        //Stuff needed for prefab merging
        public static GameObject ourPrefab { private set; get; }
        private static GameObject theirPrefab;
        public static GameObject ourPrefabInstance { private set; get; }
        private static string previouslyOpenedScene;


        public MergeManagerPrefab(GitMergeWindow window, VCS vcs)
            : base(window, vcs)
        {

        }

        public bool InitializeMerge(GameObject prefab)
        {
            if(!EditorApplication.SaveCurrentSceneIfUserWantsTo())
            {
                return false;
            }

            isMergingScene = false;
            MergeAction.inMergePhase = false;

            ObjectDictionaries.Clear();

            //checkout "their" version
            GetTheirVersionOf(AssetDatabase.GetAssetOrScenePath(prefab));
            AssetDatabase.Refresh();

            ourPrefab = prefab;

            //Open a new Scene that will only display the prefab
            previouslyOpenedScene = EditorApplication.currentScene;
            EditorApplication.NewScene();

            //make the new scene empty
            Object.DestroyImmediate(Camera.main.gameObject);

            //instantiate our object in order to view it while merging
            ourPrefabInstance = PrefabUtility.InstantiatePrefab(prefab) as GameObject;

            //find all of "our" objects in the prefab
            var ourObjects = GetAllObjects(prefab);

            theirPrefab = AssetDatabase.LoadAssetAtPath(theirFilename, typeof(GameObject)) as GameObject;
            theirPrefab.hideFlags = HideFlags.HideAndDontSave;
            var theirObjects = GetAllObjects(theirPrefab);

            //create list of differences that have to be merged
            BuildAllMergeActions(ourObjects, theirObjects);

            if(allMergeActions.Count == 0)
            {
                AssetDatabase.DeleteAsset(theirFilename);
                OpenPreviousScene();
                window.ShowNotification(new GUIContent("No conflict found for this prefab."));
                return false;
            }
            MergeAction.inMergePhase = true;
            ourPrefabInstance.Highlight();
            return true;
        }

        /// <summary>
        /// Recursively find all GameObjects that are part of the prefab
        /// </summary>
        /// <param name="prefab">The prefab to analyze</param>
        /// <param name="list">The list with all the objects already found. Pass null in the beginning.</param>
        /// <returns>The list with all the objects</returns>
        private static List<GameObject> GetAllObjects(GameObject prefab, List<GameObject> list = null)
        {
            if(list == null)
            {
                list = new List<GameObject>();
            }

            list.Add(prefab);
            foreach(Transform t in prefab.transform)
            {
                GetAllObjects(t.gameObject, list);
            }
            return list;
        }

        /// <summary>
        /// Completes the merge process after solving all conflicts.
        /// Cleans up the scene by deleting "their" GameObjects, clears merge related data structures,
        /// executes git add scene_name.
        /// </summary>
        public override void CompleteMerge()
        {
            MergeAction.inMergePhase = false;

            //ObjectDictionaries.Clear();

            allMergeActions = null;

            //TODO: Could we explicitly just save the prefab?
            AssetDatabase.SaveAssets();

            //Mark as merged for git
            vcs.MarkAsMerged(fileName);

            //directly committing here might not be that smart, since there might be more conflicts

            ourPrefab = null;

            //delete their prefab file
            AssetDatabase.DeleteAsset(theirFilename);

            OpenPreviousScene();
            window.ShowNotification(new GUIContent("Prefab successfully merged."));
        }

        /// <summary>
        /// Aborts merge by using "our" version in all conflicts.
        /// Cleans up merge related data.
        /// </summary>
        public override void AbortMerge()
        {
            base.AbortMerge();

            //delete prefab file
            AssetDatabase.DeleteAsset(theirFilename);
            OpenPreviousScene();
            ourPrefab = null;
        }

        /// <summary>
        /// Opens the previously opened scene, if there was any.
        /// </summary>
        private static void OpenPreviousScene()
        {
            if(!string.IsNullOrEmpty(previouslyOpenedScene))
            {
                EditorApplication.OpenScene(previouslyOpenedScene);
                previouslyOpenedScene = "";
            }
        }
    }
}
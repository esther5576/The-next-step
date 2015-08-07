using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BuildingExplication : MonoBehaviour
{
	public GameObject _BuildingExplanation;
	// Use this for initialization
	void Start ()
	{
		_BuildingExplanation = GameObject.Find ("BuildingExplanation");
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void Water ()
	{
		_BuildingExplanation.GetComponent<Text> ().text = "<b>Water Mine</b>\n\nWater Mines heat frozen water from Mars’s crust, extract the vapour, and purify the resulting condensed water for human use. There is no liquid water here; it is only found frozen underground, and in large quantities at the polar icecaps.\n\nWater Mines also produce Oxygen, through electrolysis: the decomposition of water in Oxygen and Hydrogen molecules via an electrical current passing through it.";
	}

	public void Landing ()
	{
		_BuildingExplanation.GetComponent<Text> ().text = "<b>Landing zone</b>\n\nThe landing zone manages the human and material resources sent from Earth. Every 30 days, a new expedition will arrive in Mars orbit from Earth, bringing supplies and 5 new colonists. They will only land if you have enough room in your Depots and Habitation Modules.";
	}

	public void Human ()
	{
		_BuildingExplanation.GetComponent<Text> ().text = "<b>Human habitats</b>\n\nLiving Quarters provide shelter to colonists when they are not going about their duties around the settlement. There, they find rest, recreation, relaxation, and intimacy; effectively, a home here on Mars.";
	}

	public void Warehouse ()
	{
		_BuildingExplanation.GetComponent<Text> ().text = "<b>Warehouse</b>\n\nDepots are used to store resources. Each one increases the total amount of each resource the colony is able to store by 20% of the Master Module’s capacity.";
	}

	public void Rover ()
	{
		_BuildingExplanation.GetComponent<Text> ().text = "<b>Rover garage</b>\n\nThe Rover Garage is a module that houses, maintains, and controls a rover for exploration and resource exploitation missions.\n\nRovers are remote controlled from the base, but also house onboard controls, and can thus be driven directly by humans is necessary. However, they do not provide the same level of radiation shielding as the base; driving it is therefore undesirable for lengthy expeditions.";
	}

	public void Com ()
	{
		_BuildingExplanation.GetComponent<Text> ().text = "<b>Communication center</b>\n\nThe Communications Tower is the colony’s link to the human satellites orbiting Mars and, from there, to Earth.\n\nThe Tower’s other major function is to broadcast and relay signals to the rovers when they are out on expeditions.\n\nAt light speed, communications signals between Mars and Earth have a delay between 4 and 24 minutes long (each way). The variation in delay comes from the variation in the two planets’ position relative to one another.\n\nFace to face conversations between the two are now filled with record amounts of awkward pauses.";
	}

	public void SolarPan ()
	{
		_BuildingExplanation.GetComponent<Text> ().text = "<b>Solar panels</b>\n\nSolar panels are the electricity generators of your colony. Although Mars is roughly 1.5 times the distance from the Sun compared to Earth, solar power remains the cheapest and most reliably abundant form of energy.\n\nIts main limitations are variations in power output depending on insolation - the amount of solar radiation energy received. The day/night cycle, positioning close to / far from the equator, as well as the frequent, and sometimes months-long dust storms, have a strong impact on the amount of electricity produced.\n\nExcess power is stored in a flywheel system: the flywheels are powered by the solar panels, and store rotational energy; this rotational energy is then converted back into electricity when solar power is insufficient to power the base.";
	}

	public void Farm ()
	{
		_BuildingExplanation.GetComponent<Text> ().text = "<b>Hydroponic farm</b>\n\nHydroponic farms produce two vital resources for human survival: the plants provide food, and oxygen through photosynthesis.\n\nHydroponics involves growing plants in water, without soil, using nutrient solutions instead. Cultivating crops using Martian soil is desirable in the long term, and was deemed theoretically possible, through experiments on Earth simulating Martian soil. However, it was impossible to be sure whether plants would actually grow in actual Martian soil before arriving and trying firsthand. One of the major long term objectives of the colony is to achieve this…";
	}

	public void Medical ()
	{
		_BuildingExplanation.GetComponent<Text> ().text = "<b>Medical center</b>\n\nThe Medibay serves several purposes. It holds the traditional role of a sick bay: housing, treating and, if necessary, quarantining sick colonists. It is also able to 3D print living tissue, thanks to the last decades of advances in medical 3D printing technology.\n\nFinally, rather than needing to bring pharmaceutical products in vast quantities from Earth, its synthetic biology lab is able to engineer and produce ad-hoc treatments on site: printing DNA and inserting it into bacterial chassis which excrete specific drugs, editing viruses so that they will attack diseases, etc. These solutions are researched/developed on Earth and produced here.";
	}
}

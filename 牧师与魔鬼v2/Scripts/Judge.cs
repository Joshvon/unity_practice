using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Judge {
	LandModel start_land;
	LandModel end_land;
	BoatModel boat;
	public Judge(LandModel start_,LandModel end_,BoatModel boat_)
	{
		start_land = start_;
		end_land = end_;
		boat = boat_;
	}
	public int Check()
	{
		int start_priest = (start_land.GetRoleNum())[0];
		int start_devil = (start_land.GetRoleNum())[1];
		int end_priest = (end_land.GetRoleNum())[0];
		int end_devil = (end_land.GetRoleNum())[1];

		if (end_priest + end_devil == 6)     
			return 2;

		int[] boat_role_num = boat.GetRoleNumber();
		if (boat.GetBoatSign() == 1)         
		{
			start_priest += boat_role_num[0];
			start_devil += boat_role_num[1];
		}
		else                                  
		{
			end_priest += boat_role_num[0];
			end_devil += boat_role_num[1];
		}
		if (start_priest > 0 && start_priest < start_devil) 
		{      
			return 1;
		}
		if (end_priest > 0 && end_priest < end_devil)        
		{
			return 1;
		}
		return 0;                                             
	}
}
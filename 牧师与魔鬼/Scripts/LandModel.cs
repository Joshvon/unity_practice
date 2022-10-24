using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandModel
	{
		GameObject land;                                
		Vector3[] positions;                            
		int land_sign;                                  
		RoleModel[] roles = new RoleModel[6];       
            
		public LandModel(string land_mark)
		{
			positions = new Vector3[] {new Vector3(46F,14.73F,-4), new Vector3(55,14.73F,-4), new Vector3(64F,14.73F,-4),
				new Vector3(73F,14.73F,-4), new Vector3(82F,14.73F,-4), new Vector3(91F,14.73F,-4)};
			if (land_mark == "start")
			{
				land = Object.Instantiate(Resources.Load("Prefabs/Land", typeof(GameObject)), new Vector3(70, 1, 0), Quaternion.identity) as GameObject;
				land_sign = 1;
			}
			else if(land_mark == "end")
			{
				land = Object.Instantiate(Resources.Load("Prefabs/Land", typeof(GameObject)), new Vector3(-70, 1, 0), Quaternion.identity) as GameObject;
				land_sign = -1;
			}
		}

		public int GetEmptyNumber()                      
		{
			for (int i = 0; i < roles.Length; i++)
			{
				if (roles[i] == null)
					return i;
			}
			return -1;
		}

		public int GetLandSign() { return land_sign; }

		public Vector3 GetEmptyPosition()               
		{
			Vector3 pos = positions[GetEmptyNumber()];
			pos.x = land_sign * pos.x;                  
			return pos;
		}

		public void AddRole(RoleModel role)             
		{
			roles[GetEmptyNumber()] = role;
		}

		public RoleModel DeleteRoleByName(string role_name)      
		{ 
			for (int i = 0; i < roles.Length; i++)
			{
				if (roles[i] != null && roles[i].GetName() == role_name)
				{
					RoleModel role = roles[i];
					roles[i] = null;
					return role;
				}
			}
			return null;
		}

		public int[] GetRoleNum()
		{
			int[] count = { 0, 0 };                    
			for (int i = 0; i < roles.Length; i++)
			{
				if (roles[i] != null)
				{
					if (roles[i].GetSign() == 0)
						count[0]++;
					else
						count[1]++;
				}
			}
			return count;
		}	
	}

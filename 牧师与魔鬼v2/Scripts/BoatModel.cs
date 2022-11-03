using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using conInterface;

public class BoatModel
	{
		public float move_speed = 250;
		GameObject boat;                                          
		Vector3[] start_empty_pos;                                    
		Vector3[] end_empty_pos;                                                                               
		ClickGUI click;
		int boat_sign = 1;                                                     
		RoleModel[] roles = new RoleModel[2];                                  

		public BoatModel()
		{
			boat = Object.Instantiate(Resources.Load("Prefabs/Boat", typeof(GameObject)), new Vector3(25, -2.5F, 0), Quaternion.identity) as GameObject;

			boat.name = "boat";
			click = boat.AddComponent(typeof(ClickGUI)) as ClickGUI;
			click.SetBoat(this);
			start_empty_pos = new Vector3[] { new Vector3(18, 4, 0), new Vector3(32,4 , 0) };
			end_empty_pos = new Vector3[] { new Vector3(-32, 4, 0), new Vector3(-18,3 , 0) };
		}

		public GameObject getGameObject() { return boat; } 

		public bool IsEmpty()
		{
			for (int i = 0; i < roles.Length; i++)
			{
				if (roles[i] != null)
					return false;
			}
			return true;
		}

		public Vector3 BoatMove()
		{
			if (boat_sign == -1)
			{
				boat_sign = 1;
                return new Vector3(25, -2.5F, 0);
			}
			else
			{
				boat_sign = -1;
                return new Vector3(-25, -2.5F, 0);
			}
		}

		public int GetBoatSign(){ return boat_sign;}

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

		public int GetEmptyNumber()
		{
			for (int i = 0; i < roles.Length; i++)
			{
				if (roles[i] == null)
				{
					return i;
				}
			}
			return -1;
		}

		public Vector3 GetEmptyPosition()
		{
			Vector3 pos;
			if (boat_sign == -1)
				pos = end_empty_pos[GetEmptyNumber()];
			else
				pos = start_empty_pos[GetEmptyNumber()];
			return pos;
		}

		public void AddRole(RoleModel role)
		{
			roles[GetEmptyNumber()] = role;
		}

		public GameObject GetBoat(){ return boat; }

		public int[] GetRoleNumber()
		{
			int[] count = { 0, 0 };
			for (int i = 0; i < roles.Length; i++)
			{
				if (roles[i] == null)
					continue;
				if (roles[i].GetSign() == 0)
					count[0]++;
				else
					count[1]++;
			}
			return count;
		}
	}

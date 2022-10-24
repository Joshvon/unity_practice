using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RoleModel
	{
		GameObject role;
		int role_sign;             
		ClickGUI click;
		bool on_boat;              
		Move move;
		LandModel land_model = (SSDirector.GetInstance().CurrentScenceController as Controller).start_land;

		public RoleModel(string role_name)
		{
			if (role_name == "priest")
			{
				role = Object.Instantiate(Resources.Load("Prefabs/Priest", typeof(GameObject)), Vector3.zero, Quaternion.Euler(0, -90, 0)) as GameObject;
				role_sign = 0;
			}
			else
			{
				role = Object.Instantiate(Resources.Load("Prefabs/Devil", typeof(GameObject)), Vector3.zero, Quaternion.Euler(0, -90, 0)) as GameObject;
				role_sign = 1;
			}
			move = role.AddComponent(typeof(Move)) as Move;
			click = role.AddComponent(typeof(ClickGUI)) as ClickGUI;
			click.SetRole(this);
		}
		public int GetSign() { return role_sign;}
		public LandModel GetLandModel(){return land_model;}
		public string GetName() { return role.name; }
		public bool IsOnBoat() { return on_boat; }
		public void SetName(string name) { role.name = name; }
		public void SetPosition(Vector3 pos) { role.transform.position = pos; }
		public void Move(Vector3 vec)
		{
			move.MovePosition(vec);
		}
		public void GoLand(LandModel land)
		{  
			role.transform.parent = null;
			land_model = land;
			on_boat = false;
		}
		public void GoBoat(BoatModel boat)
		{
			role.transform.parent = boat.GetBoat().transform;
			land_model = null;          
			on_boat = true;
		}

	}

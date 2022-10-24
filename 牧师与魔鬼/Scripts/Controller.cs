using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using conInterface;

public class Controller : MonoBehaviour, ISceneController, IUserAction
{
	public LandModel start_land;            
	public LandModel end_land;              
	public BoatModel boat;                  
	private RoleModel[] roles;              
	UserGUI user_gui;
	void Start ()
	{
		SSDirector director = SSDirector.GetInstance();
		director.CurrentScenceController = this;
		user_gui = gameObject.AddComponent<UserGUI>() as UserGUI;
		LoadResources();
	}

	public void LoadResources()              
	{
		GameObject water = Instantiate(Resources.Load("Prefabs/Water", typeof(GameObject)), new Vector3(0,-10,-2), Quaternion.identity) as GameObject;
		water.name = "water";       
		start_land = new LandModel("start");
		end_land = new LandModel("end");
		boat = new BoatModel();
		roles = new RoleModel[6];
		for (int i = 0; i < 3; i++)
		{
			RoleModel role = new RoleModel("priest");
			role.SetName("priest" + i);
			role.SetPosition(start_land.GetEmptyPosition());
			role.GoLand(start_land);
			start_land.AddRole(role);
			roles[i] = role;
		}

		for (int i = 3; i < 6; i++)
		{
			RoleModel role = new RoleModel("devil");
			role.SetName("devil" + i);
			role.SetPosition(start_land.GetEmptyPosition());
			role.GoLand(start_land);
			start_land.AddRole(role);
			roles[i] = role;
		}
	}

	public void MoveBoat()                  
	{
		if (boat.IsEmpty() || user_gui.sign != 0) return;
		boat.BoatMove();
		user_gui.sign = Check();
	}

	public void MoveRole(RoleModel role)    
	{
		if (user_gui.sign != 0) return;
		if (role.IsOnBoat())
		{
			LandModel land;
			if (boat.GetBoatSign() == -1)
				land = end_land;
			else
				land = start_land;
			boat.DeleteRoleByName(role.GetName());
			role.Move(land.GetEmptyPosition());
			role.GoLand(land);
			land.AddRole(role);
		}
		else
		{                                
			LandModel land = role.GetLandModel();
			if (boat.GetEmptyNumber() == -1 || land.GetLandSign() != boat.GetBoatSign()) return;   
			land.DeleteRoleByName(role.GetName());
			role.Move(boat.GetEmptyPosition());
			role.GoBoat(boat);
			boat.AddRole(role);
		}
		user_gui.sign = Check();
	}

	public void Restart()
	{
		SceneManager.LoadScene(0);
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

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
	public MySceneActionManager actionManager;
	private Judge judge;     
	UserGUI user_gui;
	void Start ()
	{
		SSDirector director = SSDirector.GetInstance();
		director.CurrentScenceController = this;
		user_gui = gameObject.AddComponent<UserGUI>() as UserGUI;
		LoadResources();
		actionManager = gameObject.AddComponent<MySceneActionManager>() as MySceneActionManager;
		judge = new Judge (start_land,end_land,boat);
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
        actionManager.moveBoat (boat.getGameObject(),boat.BoatMove(),boat.move_speed);
        user_gui.sign = judge.Check();
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
            actionManager.moveRole(role.getGameObject(),new Vector3(role.getGameObject().transform.position.x,land.GetEmptyPosition ().y,land.GetEmptyPosition ().z),land.GetEmptyPosition (),role.move_speed);
            role.GoLand(land);
            land.AddRole(role);

        }
        else
        {
            LandModel land = role.GetLandModel();
            if (boat.GetEmptyNumber() == -1 || land.GetLandSign() != boat.GetBoatSign()) return;   
            land.DeleteRoleByName(role.GetName());
            actionManager.moveRole(role.getGameObject(),new Vector3(boat.GetEmptyPosition().x,role.getGameObject().transform.position.y,boat.GetEmptyPosition().z),boat.GetEmptyPosition (),role.move_speed);
            role.GoBoat(boat);
            boat.AddRole(role);
        }
        user_gui.sign = judge.Check();
	}

	public void Restart()
	{
		SceneManager.LoadScene(0);
	}

	public int Check() {return 0;}
}

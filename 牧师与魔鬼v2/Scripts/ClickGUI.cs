using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using conInterface;

public class ClickGUI : MonoBehaviour
{
    IUserAction action;
	RoleModel role = null;
	BoatModel boat = null;
    // Start is called before the first frame update
	void Start()
	{
		action = SSDirector.GetInstance().CurrentScenceController as IUserAction;
	}

    public void SetRole(RoleModel role)
	{
		this.role = role;
	}
	public void SetBoat(BoatModel boat)
	{
		this.boat = boat;
	}
	void OnMouseDown()
	{
		if (boat == null && role == null) return;
		if (boat != null)
			action.MoveBoat();
		else if(role != null)
			action.MoveRole(role);
	}
}

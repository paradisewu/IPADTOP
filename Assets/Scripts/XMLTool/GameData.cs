using System.Collections.Generic;

public class GameData
{
    private string iD;

    public string ID
    {
        get { return iD; }
        set { iD = value; }
    }
}
public class MyPerson : GameData
{

    private string name;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    private int sort;

    public int Sort
    {
        get { return sort; }
        set { sort = value; }
    }
    private float life;

    public float Life
    {
        get { return life; }
        set { life = value; }
    }
}

public class ControlData : GameData
{
    public ControlType Control_Type { get; set; }

    public List<ProjectData> ProjectDatas { get; set; }
}

public class ProjectData
{
    public int Id { get; set; }

    public string Name { get; set; }
}
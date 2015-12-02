class MySingletonClass
{
	//initializes a null singleton
	private static var Instance : MySingletonClass = null;
	
	//member variables
	public static var initialRoom : boolean = false;
    public static var redRoom : boolean = false;
	public static var afterRedRoom : boolean = false;
	public static var greenRoom : boolean = false;
	public static var afterGreenRoom : boolean = false;
	public static var blueRoom : boolean = true;
	public static var afterBlueRoom : boolean = false;

    public static var MAX_LIVES : int = 5;
    public static var playerLives : int = MAX_LIVES;			//number of lives the player has
    public static var numEnemiesKilled : int = 0;
	
	//creates new instance of the singleton
	public static function GetInstance(): MySingletonClass
	{
		if (Instance == null)
			Instance = new MySingletonClass();
		
   		return Instance;
	}

	private function MySingletonClass()
	{
    	if(Instance!=null)
    	{
        	//Debug.Log("this a singleton class, use MySingletonInstance.GetInstance() instead");
    	}
	}

}
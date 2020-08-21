using UnityEngine;


public class PlayerInfos : MonoBehaviour
{
	public int maxAIdefeated = 0;
	public int maxPriceAllowed = 60;

	private static PlayerInfos _instance;
	private SaveLoad SnL;


	// make this a singleton
	void Awake()
	{

		if (_instance == null)
		{
			_instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
			Destroy(gameObject);

		string s;

		SnL = new SaveLoad();
		try
		{
			s = SnL.LoadPlayer();
			string AIs = "";
			string Price = "";
			ushort u = 0;
			for (int i = 0; i < s.Length; i++)
			{
				if (s[i] == ':')
				{
					u++;
					continue;
				}

				if (u == 0)
					AIs += s[i];
				else
					Price += s[i];
            }

			maxAIdefeated = int.Parse(AIs);
			maxPriceAllowed = int.Parse(Price);
		}
		catch
		{
			SnL.SavePlayer("0:60");
		}
	}


	public void ChangeAIdefeated(int nb)
    {
		string NewPrice = "60";
		if (nb == 1)
			NewPrice = "70";
		if (nb == 2)
			NewPrice = "90";

		maxAIdefeated = nb;
		maxPriceAllowed = int.Parse(NewPrice);
		SnL.SavePlayer(nb.ToString() + ":" + NewPrice);
    }
}
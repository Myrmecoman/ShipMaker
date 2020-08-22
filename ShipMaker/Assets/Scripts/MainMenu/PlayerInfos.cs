using UnityEngine;


public class PlayerInfos : MonoBehaviour
{
	public int maxAIdefeated = 0;
	public int maxPriceAllowed = 60;
	public int lastAiID = 0;

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

			lastAiID = int.Parse(AIs);
			maxAIdefeated = int.Parse(AIs);
			maxPriceAllowed = int.Parse(Price);
		}
		catch
		{
			Debug.Log("First time connecting, or failed, creating new player.p");
			SnL.SavePlayer("0:60");
			maxAIdefeated = 0;
			maxPriceAllowed = 60;
			lastAiID = 0;
		}
	}


	public void ChangeAIdefeated()
    {
		if (lastAiID <= maxAIdefeated)
			return;

		int NewPrice = 60;
		if (lastAiID == 1)
			NewPrice = 70;
		if (lastAiID == 2)
			NewPrice = 85;
		if (lastAiID == 3)
			NewPrice = 100;
		if (lastAiID == 4)
			NewPrice = 120;
		if (lastAiID == 5)
			NewPrice = 150;
		if (lastAiID == 6)
			NewPrice = 180;
		if (lastAiID == 7)
			NewPrice = 200;

		maxAIdefeated = lastAiID;
		maxPriceAllowed = NewPrice;
		SnL.SavePlayer(lastAiID + ":" + NewPrice);
    }
}
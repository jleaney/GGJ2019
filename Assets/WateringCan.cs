using UnityEngine;

public class WateringCan : Pickup
{
	public override bool Held
	{
		set
		{
			_watering = value;
			if(value) particles.Play();
			else particles.Stop();
		}
	}
	
	private void Update()
	{
		
	}

	private bool _watering;
	[SerializeField] private ParticleSystem particles;
}


using UnityEngine;
[System.Serializable]
public class CoolDownEvent {

	public float lastGoTime;
	public float cooldownTime;

	public CoolDownEvent(float cooldown)
	{
		lastGoTime = -cooldown;
		cooldownTime = cooldown;
	}

	public CoolDownEvent()
	{
		lastGoTime = -1f;
		cooldownTime = 1f;
	}

	public bool TryGo(float cd = -1f)
	{
		if (Time.time > (lastGoTime+cooldownTime))
		{
			if (cd != -1f)
				cooldownTime = cd;

			lastGoTime = Time.time;
			return true;
		}
		return false;
	}

	public bool Go(float cd = -1f)
	{
		if (Time.time > (lastGoTime+cooldownTime))
		{
			if (cd != -1f)
				cooldownTime = cd;

			lastGoTime = Time.time;
			return true;
		}

		if (cd != -1f)
			cooldownTime = cd;

		lastGoTime = Time.time;
		return false;
	}

	public bool Available()
	{
		if (Time.time > (lastGoTime+cooldownTime))
			return true;
		return false;
	}

	public bool GoneForGreaterThan(float time)
	{
		if (Time.time > (lastGoTime+time))
			return true;
		return false;
	}

	public float Get01Progress()
	{
		return (Time.time-lastGoTime)/(cooldownTime <= 0f ? 1f : cooldownTime);
	}

	public float GetTimeLeft()
	{
		return (lastGoTime+cooldownTime)-Time.time;
	}
}
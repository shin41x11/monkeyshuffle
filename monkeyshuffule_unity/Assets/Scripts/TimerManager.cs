using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Timer関連のマネージャ
public class TimerManager : MonoBehaviour
{
	[SerializeField]
	private int minute;
	[SerializeField]
	private float seconds;
	//　前のUpdateの時の秒数
	private float oldSeconds;
	//　タイマー表示用テキスト
	[SerializeField]	private Text timerText;
 
 	[SerializeField]
	private Tree[] trees;

 	[SerializeField]
	 Slider clockslider;

	 const int counterRotationSeconds = 5;

	void Start () {
		minute = 0;
		seconds = 0f;
		oldSeconds = 0f;
	}
 
	void Update () {
		seconds += Time.deltaTime;
		if(seconds >= 60f) {
			minute++;
			seconds = seconds - 60;
		}

		//　テキストUI更新(値が変わった時だけ)
		if((int)seconds != (int)oldSeconds) {
			timerText.text = minute.ToString("00") + ":" + ((int) seconds).ToString ("00");
		}
		oldSeconds = seconds;

		// 回転の向きを取得(1 or -1にする)
		int direction = 1;

		if((int)seconds % (counterRotationSeconds * 2) >= counterRotationSeconds) {
			direction = -1;
		}

		//時計を更新
		clockslider.value = 1 - Mathf.Abs(seconds % (counterRotationSeconds * 2) - counterRotationSeconds) /counterRotationSeconds;

		// 木を回転
		foreach (var tree in trees)
		{
			tree.rotate(direction);
		}

		
	}

	public int getSeconds() {
		return (int)seconds;
	}
}

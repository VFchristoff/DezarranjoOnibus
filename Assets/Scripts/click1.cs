using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class click1 : MonoBehaviour {
	// Precisa de duas variáveis inteiras
	// uma pra cada botão e uma pro todo (static)
	public static int TotalCounts = 0;
	public int ClickCount = 0;

	// Precisa de duas referências de texto
	// uma pro botão e uma pro display;
	public UnityEngine.UI.Text ClickDisplay;
	public UnityEngine.UI.Text ClickProprio;

	private GameManager manager;

	public enum Direcao { Esquerda, Direita }
	public Direcao direcaoDoBotao;



	void Start () {
		manager = GameObject.Find ("GameManager").GetComponent <GameManager> ();
	}
	public void ClickTheButton () 
		{
		ClickCount += 1;
		TotalCounts += 1;
		// Na variavel ClickDisplay você exibe o ClickCount
		// Na variavel ClickDisplayDoBotao você exibe o ClickCountDoBotao
		//Debug.Log ("ClickCount: " + ClickCount);
		//ClickDisplay.text = "Total Counts" + TotalCounts;
		ClickProprio.text = "Click" + ClickCount;

	
		if (direcaoDoBotao == Direcao.Esquerda) {
			manager.Click (false);
		} else if (direcaoDoBotao == Direcao.Direita) {
			manager.Click (true);
		}
	}
}

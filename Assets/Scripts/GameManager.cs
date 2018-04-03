using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {


	//CONTADOR DO CONTADOR DE VIDAS DO JOGADOR
	private float contadorDeVidas;
	public float maximoDeVidas = 5;
	public Text vidasContador;

	//CONTADOR
	public float intervalo = 5f;
	private float contador;
	//variavel de passageiros no onibus


	//BOOL PARA ATIVAR O MENU
	private bool menuAtivado;

	//BUSCA O SPRITE DO ONIBUS, O MENU E O PREFAB
	public GameObject passageiro;
	private GameObject busao;
	private GameObject menu;
	private GameObject canvas;

	//CONDICAO DE DERROTA
	public float forceY = 10f;
	public float forceX = 20f;
	public float deathZone = 30;
	public Text GameOver;

	//TIMER
	public Text timerText;
	private float secondsCount = 0f;
	private int minuteCount = 0;


	//START
	void Start () 
	{
		contador = intervalo;
		busao = GameObject.Find ("BusaoSprite");
		menu = GameObject.Find ("Menu");
		canvas = GameObject.Find ("Canvas");

		menuAtivado = true;
		passageiro.GetComponent<Destroy> ().gameManager = this.gameObject;
	}
		

	//UPDATE
	void Update() {
		
		if (menuAtivado) {
			return;
		}



		//TIMER
		UpdateTimerUI ();
		InclinacaoMaxima ();
		if (contadorDeVidas < 0) {
			GameOver.text = "Game Over!";
			menuAtivado = true;
			menu.SetActive (true);

		}

		vidasContador.text = "Vidas: " + (contadorDeVidas + 1);

		//CONTADOR pra inclinar aleatoriamente
		contador -= Time.deltaTime;
		if (contador <= 0.0f) {
			
			//PESSOA VOA DO ÔNIBUS
			//reduz o número de passageiros (variavel de passageiros do onibus)
			mecanicaDoPassageiro ();
			//instancia um botão
			//move o botão <-
			// INCLINA
			float inclinacao = 10f;
			//passageiro se deslocando do centro para x angulo

			if (Random.value > 0.5f) {
				inclinacao *= -1;
				//passageiro se deslocando do centro para x angulo
			}

			InclinarOnibus (inclinacao);
			contador = intervalo*Random.Range (0.7f, 1.3f);

		}

	}
	private void mecanicaDoPassageiro () {
		GameObject objetoDoPassageiro = Instantiate(passageiro, new Vector3 (0,0.5f,0), Quaternion.Euler(0,0,45), canvas.transform);
		objetoDoPassageiro.GetComponent <Rigidbody2D> ().AddForce (new Vector2 (forceX*Random.Range (-1f,1f), forceY));
	}

	//MENU
	public void botaoMenu () {
		menuAtivado = false;
		menu.SetActive (false);
		busao.transform.eulerAngles = new Vector3 (0,0,0);
		contadorDeVidas = maximoDeVidas;
		secondsCount = 0;
		minuteCount = 0;
		contador = intervalo;
		GameOver.text = "";


	}

	/// <summary>
	/// Adiciona um valor no número de Vidas do Jogador.
	/// Pode ser negativo.
	/// </summary>
	/// <param name="valor">valor</param>
	public void atualizarVida (int valor) {
		contadorDeVidas += valor;

		//O objeto que tá voando - a pessoa - já foi destruída
		//aumentar o número de passageiros

		//fazer contador e quando o objeto for clicado, ele ganha 1
	}






	//ROTACAO DO CLICK
	public void Click (bool opcao) {

		if (menuAtivado) {
			return;
		}

		if (opcao) {
			// opcao == true
			InclinarOnibus (10f);
			// rotaciona pra direita
		} else if (!opcao) {
			// opcao == false
			InclinarOnibus (-10f);
			// rotaciona pra esquerda
		}
	}


	/// <summary>
	/// Inclina o onibus.
	/// </summary>
	/// <param name="inclinacao">Quantidade de inclinação.</param>
	private void InclinarOnibus (float inclinacao) {
		Vector3 rotacaoNova = new Vector3 (0, 0, busao.transform.rotation.z + inclinacao);
		busao.transform.Rotate (rotacaoNova);
	}


	//CONDICAO DE DERROTA
	private void InclinacaoMaxima () {
		if ((busao.transform.eulerAngles.z > deathZone) && 
			(busao.transform.eulerAngles.z < (360f - deathZone))
		) {
			GameOver.text = "Game Over!";
			menuAtivado = true;
			menu.SetActive (true);
		}
	}




	//TIMER
	public void UpdateTimerUI () {
		secondsCount += Time.deltaTime;

		string var = "Tempo: " + minuteCount + ":" + (int)secondsCount;
		timerText.text = var;
			
		if (secondsCount >= 60) {
			minuteCount++;
			secondsCount = 00f;
		} else if (minuteCount >= 60) {
			minuteCount = 00;
		}
	}
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseManager : MonoBehaviour {

	//Variables publicas
	public int numeroCartas = 23;
	public Sprite dorsoSprite;
	public Sprite vueltaCartaSprite;
	public Vector2 posicionInicial;
	public Vector2 posicionMazo;
	public Vector2 primerMonton;
	public Vector2 segundoMonton;
	public Vector2 tercerMonton;
	public Vector2 cuartoMonton;
	public Vector2 quintoMonton;
	public Vector2 sextoMonton;
	public Vector2 septimoMonton;

	public List<Sprite> posiblesCartas = new List<Sprite>();

	//Variables privadas
	List<GameObject> cartas = new List<GameObject>();
	List<GameObject> dorsos = new List<GameObject>();
	List<GameObject> cartasMonton1 = new List<GameObject>();
	List<GameObject> cartasMonton2 = new List<GameObject>();
	List<GameObject> cartasMonton3 = new List<GameObject>();
	List<GameObject> cartasMonton4 = new List<GameObject>();
	List<GameObject> cartasMonton5 = new List<GameObject>();
	List<GameObject> cartasMonton6 = new List<GameObject>();
	List<GameObject> cartasMonton7 = new List<GameObject>();
	List<GameObject> montonPicas = new List<GameObject>();
	List<GameObject> montonTreboles = new List<GameObject>();
	List<GameObject> montonCorazones = new List<GameObject>();
	List<GameObject> montonDiamantes = new List<GameObject>();

	List<GameObject>[] montones = new List<GameObject>[7];
	Vector2[] posicionMontones = new Vector2[7];
	bool[] picas = new bool[13];
	bool[] treboles = new bool[13];
	bool[] corazones = new bool[13];
	bool[] diamantes = new bool[13];

	List<Sprite>[] spritesMontones = new List<Sprite>[7];
	List<Sprite> spritesMonton1 = new List<Sprite>();
	List<Sprite> spritesMonton2 = new List<Sprite>();
	List<Sprite> spritesMonton3 = new List<Sprite>();
	List<Sprite> spritesMonton4 = new List<Sprite>();
	List<Sprite> spritesMonton5 = new List<Sprite>();
	List<Sprite> spritesMonton6 = new List<Sprite>();
	List<Sprite> spritesMonton7 = new List<Sprite>();

	int indiceCartaADestruir = 0;
	int indiceCartaASacar = 0;
	bool primeraVuelta = false;
	GameObject cartaCogida;
	float lastClickTime = 0.0f;
	float catchTime = 0.25f;
	int zMazo = 0;
	
	// Use this for initialization
	void Start () {
		picas [0] = false;
		treboles [0] = false;
		corazones [0] = false;
		diamantes [0] = false;

		posicionMazo = new Vector2 (-3.31f, 5f);
		posicionInicial = new Vector2 (-5f, 5f);
		primerMonton = new Vector2 (-5f, 2f);
		segundoMonton = new Vector2 (-3f, 2f);
		tercerMonton = new Vector2 (-1f, 2f);
		cuartoMonton = new Vector2 (1f, 2f);
		quintoMonton = new Vector2 (3f, 2f);
		sextoMonton = new Vector2 (5f, 2f);
		septimoMonton = new Vector2 (7f, 2f);

		montones [0] = cartasMonton1;
		montones [1] = cartasMonton2;
		montones [2] = cartasMonton3;
		montones [3] = cartasMonton4;
		montones [4] = cartasMonton5;
		montones [5] = cartasMonton6;
		montones [6] = cartasMonton7;

		posicionMontones [0] = primerMonton;
		posicionMontones [1] = segundoMonton;
		posicionMontones [2] = tercerMonton;
		posicionMontones [3] = cuartoMonton;
		posicionMontones [4] = quintoMonton;
		posicionMontones [5] = sextoMonton;
		posicionMontones [6] = septimoMonton;

		spritesMontones [0] = spritesMonton1;
		spritesMontones [1] = spritesMonton2;
		spritesMontones [2] = spritesMonton3;
		spritesMontones [3] = spritesMonton4;
		spritesMontones [4] = spritesMonton5;
		spritesMontones [5] = spritesMonton6;
		spritesMontones [6] = spritesMonton7;

		creacionMazos ();

		//Mazo
		for (int i = 0; i < posiblesCartas.Count; i++) {
			GameObject dorso = new GameObject();
			dorso.name = "Dorso" + i;
			SpriteRenderer spriteRender = dorso.AddComponent<SpriteRenderer>();
			BoxCollider2D coll = dorso.AddComponent<BoxCollider2D>();
			coll.size = new Vector2(1.4f, 1.9f);
			spriteRender.sprite = dorsoSprite;
			dorso.tag = "dorso";
			dorso.transform.position = new Vector2(posicionInicial.x, posicionInicial.y + 0.01f * i);
			dorsos.Add(dorso);
		}

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown (0)) {

			Vector3 mouseWorld3d = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 mouse2dPos = new Vector2(mouseWorld3d.x, mouseWorld3d.y);

			Vector2 dir = Vector2.zero;
			RaycastHit2D hit = Physics2D.Raycast(mouse2dPos, dir);
			if(hit.collider != null){
				Debug.Log(hit.collider.gameObject.name);
				if(Time.time - lastClickTime < catchTime && hit.collider.gameObject.tag != "dorso" ){
					if(hit.collider.gameObject.name.Contains("SpadesA") && !picas[0]){
						siguientePica(hit.collider.gameObject, 0);
					} 
					if(hit.collider.gameObject.name.Contains("Spades2") && picas[0]){
						siguientePica(hit.collider.gameObject, 1);
					} 
					if(hit.collider.gameObject.name.Contains("Spades3") && picas[1]){
						siguientePica(hit.collider.gameObject, 2);
					} 
					if(hit.collider.gameObject.name.Contains("Spades4") && picas[2]){
						siguientePica(hit.collider.gameObject, 3);
					} 
					if(hit.collider.gameObject.name.Contains("Spades5") && picas[3]){
						siguientePica(hit.collider.gameObject, 4);
					}
					if(hit.collider.gameObject.name.Contains("Spades6") && picas[4]){
						siguientePica(hit.collider.gameObject, 5);
					}
					if(hit.collider.gameObject.name.Contains("Spades7") && picas[5]){
						siguientePica(hit.collider.gameObject, 6);
					}
					if(hit.collider.gameObject.name.Contains("Spades8") && picas[6]){
						siguientePica(hit.collider.gameObject, 7);
					}
					if(hit.collider.gameObject.name.Contains("Spades9") && picas[7]){
						siguientePica(hit.collider.gameObject, 8);
					}
					if(hit.collider.gameObject.name.Contains("Spades10") && picas[8]){
						siguientePica(hit.collider.gameObject, 9);
					}
					if(hit.collider.gameObject.name.Contains("SpadesJ") && picas[9]){
						siguientePica(hit.collider.gameObject, 10);
					}
					if(hit.collider.gameObject.name.Contains("SpadesQ") && picas[10]){
						siguientePica(hit.collider.gameObject, 11);
					}
					if(hit.collider.gameObject.name.Contains("SpadesK") && picas[11]){
						siguientePica(hit.collider.gameObject, 12);
					}

					if(hit.collider.gameObject.name.Contains("ClubsA") && !treboles[0]){
						siguienteTrebol(hit.collider.gameObject, 0);
					} 
					if(hit.collider.gameObject.name.Contains("Clubs2") && treboles[0]){
						siguienteTrebol(hit.collider.gameObject, 1);
					} 
					if(hit.collider.gameObject.name.Contains("Clubs3") && treboles[1]){
						siguienteTrebol(hit.collider.gameObject, 2);
					} 
					if(hit.collider.gameObject.name.Contains("Clubs4") && treboles[2]){
						siguienteTrebol(hit.collider.gameObject, 3);
					} 
					if(hit.collider.gameObject.name.Contains("Clubs5") && treboles[3]){
						siguienteTrebol(hit.collider.gameObject, 4);
					}
					if(hit.collider.gameObject.name.Contains("Clubs6") && treboles[4]){
						siguienteTrebol(hit.collider.gameObject, 5);
					}
					if(hit.collider.gameObject.name.Contains("Clubs7") && treboles[5]){
						siguienteTrebol(hit.collider.gameObject, 6);
					}
					if(hit.collider.gameObject.name.Contains("Clubs8") && treboles[6]){
						siguienteTrebol(hit.collider.gameObject, 7);
					}
					if(hit.collider.gameObject.name.Contains("Clubs9") && treboles[7]){
						siguienteTrebol(hit.collider.gameObject, 8);
					}
					if(hit.collider.gameObject.name.Contains("Clubs10") && treboles[8]){
						siguienteTrebol(hit.collider.gameObject, 9);
					}
					if(hit.collider.gameObject.name.Contains("ClubsJ") && treboles[9]){
						siguienteTrebol(hit.collider.gameObject, 10);
					}
					if(hit.collider.gameObject.name.Contains("ClubsQ") && treboles[10]){
						siguienteTrebol(hit.collider.gameObject, 11);
					}
					if(hit.collider.gameObject.name.Contains("ClubsK") && treboles[11]){
						siguienteTrebol(hit.collider.gameObject, 12);
					}

					if(hit.collider.gameObject.name.Contains("HeartsA") && !corazones[0]){
						siguienteCorazon(hit.collider.gameObject, 0);
					} 
					if(hit.collider.gameObject.name.Contains("Hearts2") && corazones[0]){
						siguienteCorazon(hit.collider.gameObject, 1);
					} 
					if(hit.collider.gameObject.name.Contains("Hearts3") && corazones[1]){
						siguienteCorazon(hit.collider.gameObject, 2);
					} 
					if(hit.collider.gameObject.name.Contains("Hearts4") && corazones[2]){
						siguienteCorazon(hit.collider.gameObject, 3);
					} 
					if(hit.collider.gameObject.name.Contains("Hearts5") && corazones[3]){
						siguienteCorazon(hit.collider.gameObject, 4);
					}
					if(hit.collider.gameObject.name.Contains("Hearts6") && corazones[4]){
						siguienteCorazon(hit.collider.gameObject, 5);
					}
					if(hit.collider.gameObject.name.Contains("Hearts7") && corazones[5]){
						siguienteCorazon(hit.collider.gameObject, 6);
					}
					if(hit.collider.gameObject.name.Contains("Hearts8") && corazones[6]){
						siguienteCorazon(hit.collider.gameObject, 7);
					}
					if(hit.collider.gameObject.name.Contains("Hearts9") && corazones[7]){
						siguienteCorazon(hit.collider.gameObject, 8);
					}
					if(hit.collider.gameObject.name.Contains("Hearts10") && corazones[8]){
						siguienteCorazon(hit.collider.gameObject, 9);
					}
					if(hit.collider.gameObject.name.Contains("HeartsJ") && corazones[9]){
						siguienteCorazon(hit.collider.gameObject, 10);
					}
					if(hit.collider.gameObject.name.Contains("HeartsQ") && corazones[10]){
						siguienteCorazon(hit.collider.gameObject, 11);
					}
					if(hit.collider.gameObject.name.Contains("HeartsK") && corazones[11]){
						siguienteCorazon(hit.collider.gameObject, 12);
					}

					if(hit.collider.gameObject.name.Contains("DiamondsA") && !diamantes[0]){
						siguienteDiamante(hit.collider.gameObject, 0);
					} 
					if(hit.collider.gameObject.name.Contains("Diamonds2") && diamantes[0]){
						siguienteDiamante(hit.collider.gameObject, 1);
					} 
					if(hit.collider.gameObject.name.Contains("Diamonds3") && diamantes[1]){
						siguienteDiamante(hit.collider.gameObject, 2);
					} 
					if(hit.collider.gameObject.name.Contains("Diamonds4") && diamantes[2]){
						siguienteDiamante(hit.collider.gameObject, 3);
					} 
					if(hit.collider.gameObject.name.Contains("Diamonds5") && diamantes[3]){
						siguienteDiamante(hit.collider.gameObject, 4);
					}
					if(hit.collider.gameObject.name.Contains("Diamonds6") && diamantes[4]){
						siguienteDiamante(hit.collider.gameObject, 5);
					}
					if(hit.collider.gameObject.name.Contains("Diamonds7") && diamantes[5]){
						siguienteDiamante(hit.collider.gameObject, 6);
					}
					if(hit.collider.gameObject.name.Contains("Diamonds8") && diamantes[6]){
						siguienteDiamante(hit.collider.gameObject, 7);
					}
					if(hit.collider.gameObject.name.Contains("Diamonds9") && diamantes[7]){
						siguienteDiamante(hit.collider.gameObject, 8);
					}
					if(hit.collider.gameObject.name.Contains("Diamonds10") && diamantes[8]){
						siguienteDiamante(hit.collider.gameObject, 9);
					}
					if(hit.collider.gameObject.name.Contains("DiamondsJ") && diamantes[9]){
						siguienteDiamante(hit.collider.gameObject, 10);
					}
					if(hit.collider.gameObject.name.Contains("DiamondsQ") && diamantes[10]){
						siguienteDiamante(hit.collider.gameObject, 11);
					}
					if(hit.collider.gameObject.name.Contains("DiamondsK") && diamantes[11]){
						siguienteDiamante(hit.collider.gameObject, 12);
					}
				} else {
					if(hit.collider.gameObject.tag.Equals("dorso")){
						Destroy(dorsos[indiceCartaADestruir]);
						indiceCartaADestruir++;
						if(primeraVuelta == false){
							GameObject carta;
							carta = cartaAlAzar();
							carta.transform.position = new Vector3(posicionMazo.x, posicionMazo.y, zMazo - 1);
							zMazo--;
							cartas.Add(carta);

						} 
						else {
							cartas[indiceCartaASacar].SetActive(true);
							indiceCartaASacar--;
							zMazo--;
						}
						if(indiceCartaADestruir == dorsos.Count){

							vueltaCarta();
							if(!primeraVuelta){
								cartas.Reverse();
							}
							primeraVuelta = true;
							indiceCartaASacar = cartas.Count - 1;
						}
					}
					if(hit.collider.gameObject.tag.Equals("vuelta")){
						vuelta();
						zMazo = 0;
					}
					if(hit.collider.gameObject.tag.Equals("carta")){
						cartaCogida = hit.collider.gameObject;
					}
					if(hit.collider.gameObject.tag.Equals("monton1")){
						cartaCogida = hit.collider.gameObject;
					}
					if(hit.collider.gameObject.tag == "dorsoMonton1" && sePuedePulsar(0)){
						vueltaMonton(hit.collider.gameObject, 0);
					}
					if(hit.collider.gameObject.tag.Equals("monton2")){
						cartaCogida = hit.collider.gameObject;
					}
					if(hit.collider.gameObject.tag == "dorsoMonton2" && sePuedePulsar(1)){
						vueltaMonton(hit.collider.gameObject, 1);
					}
					if(hit.collider.gameObject.tag.Equals("monton3")){
						cartaCogida = hit.collider.gameObject;
					}
					if(hit.collider.gameObject.tag == "dorsoMonton3" && sePuedePulsar(2)){
						vueltaMonton(hit.collider.gameObject, 2);
					}
					if(hit.collider.gameObject.tag.Equals("monton4")){
						cartaCogida = hit.collider.gameObject;
					}
					if(hit.collider.gameObject.tag == "dorsoMonton4" && sePuedePulsar(3)){
						vueltaMonton(hit.collider.gameObject, 3);
					}
					if(hit.collider.gameObject.tag.Equals("monton5")){
						cartaCogida = hit.collider.gameObject;
					}
					if(hit.collider.gameObject.tag == "dorsoMonton5" && sePuedePulsar(4)){
						vueltaMonton(hit.collider.gameObject, 4);
					}
					if(hit.collider.gameObject.tag.Equals("monton6")){
						cartaCogida = hit.collider.gameObject;
					}
					if(hit.collider.gameObject.tag == "dorsoMonton6" && sePuedePulsar(5)){
						vueltaMonton(hit.collider.gameObject, 5);
					}
					if(hit.collider.gameObject.tag.Equals("monton7")){
						cartaCogida = hit.collider.gameObject;
					}
					if(hit.collider.gameObject.tag == "dorsoMonton7" && sePuedePulsar(6)){
						vueltaMonton(hit.collider.gameObject, 6);
					}
					if(hit.collider.gameObject.tag.Equals("montonPicas")){
						cartaCogida = hit.collider.gameObject;
					}
					if(hit.collider.gameObject.tag.Equals("montonTreboles")){
						cartaCogida = hit.collider.gameObject;
					}
					if(hit.collider.gameObject.tag.Equals("montonCorazones")){
						cartaCogida = hit.collider.gameObject;
					}
					if(hit.collider.gameObject.tag.Equals("montonDiamantes")){
						cartaCogida = hit.collider.gameObject;
					}
				}
			}
			lastClickTime = Time.time;
		} 

		if (Input.GetMouseButtonUp(0) && cartaCogida != null) {
			if(cartaCogida.tag.Equals("carta")){
				cartaCogida.transform.position = new Vector3(posicionMazo.x, posicionMazo.y, zMazo);
				cartaCogida = null;
			}
			else if(cartaCogida.tag.Equals("monton1")){
				cartaCogida.transform.position = primerMonton;
				cartaCogida = null;
			}
			else if(cartaCogida.tag.Equals("monton2")){
				cartaCogida.transform.position = new Vector3(segundoMonton.x, 
				                                             segundoMonton.y - 0.25f * (montones[1].Count - 1),
				                                             -montones[1].Count + 1);
				cartaCogida = null;
			}
			else if(cartaCogida.tag.Equals("monton3")){
				cartaCogida.transform.position = new Vector3(tercerMonton.x, 
				                                             tercerMonton.y - 0.25f * (montones[2].Count - 1),
				                                             -montones[2].Count + 1);
				cartaCogida = null;
			}
			else if(cartaCogida.tag.Equals("monton4")){
				cartaCogida.transform.position = new Vector3 (cuartoMonton.x,
				                                              cuartoMonton.y - 0.25f * (montones[3].Count - 1),
				                                              -montones[3].Count + 1);
				cartaCogida = null;
			}
			else if(cartaCogida.tag.Equals("monton5")){
				cartaCogida.transform.position = new Vector3 (quintoMonton.x, 
				                                              quintoMonton.y - 0.25f * (montones[4].Count - 1),
				                                              -montones[4].Count + 1);
				cartaCogida = null;
			}
			else if(cartaCogida.tag.Equals("monton6")){
				cartaCogida.transform.position = new Vector3 (sextoMonton.x, 
				                                              quintoMonton.y - 0.25f * (montones[5].Count - 1),
				                                              -montones[5].Count + 1);
				cartaCogida = null;
			}
			else if(cartaCogida.tag.Equals("monton7")){
				cartaCogida.transform.position = new Vector3 (septimoMonton.x, 
				                                              septimoMonton.y - 0.25f * (montones[6].Count - 1),
				                                              -montones[6].Count + 1);
				cartaCogida = null;
			}
			else if(cartaCogida.tag.Equals("montonPicas")){
				cartaCogida.transform.position = new Vector3 (posicionMazo.x + 4f, 
				                                              posicionMazo.y, 
				                                              -montonPicas.Count + 1);
				cartaCogida = null;
			}
			else if(cartaCogida.tag.Equals("montonTreboles")){
				cartaCogida.transform.position = new Vector3 (posicionMazo.x + 6f, 
				                                              posicionMazo.y, 
				                                              -montonTreboles.Count + 1);
				cartaCogida = null;
			}

			else if(cartaCogida.tag.Equals("montonCorazones")){
				cartaCogida.transform.position = new Vector3 (posicionMazo.x + 8f, 
				                                              posicionMazo.y, 
				                                              -montonCorazones.Count + 1);
				cartaCogida = null;
			}

			else if(cartaCogida.tag.Equals("montonDiamantes")){
				cartaCogida.transform.position = new Vector3 (posicionMazo.x + 10f, 
				                                              posicionMazo.y, 
				                                              -montonDiamantes.Count + 1);
				cartaCogida = null;
			}


		}
	}

	void FixedUpdate(){
		if (cartaCogida != null) {
			Vector3 mouseWorld3d = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector3 mouse2dPos = new Vector3(mouseWorld3d.x, mouseWorld3d.y,-49);

			cartaCogida.transform.position = mouse2dPos;
		}
	}

	//Crea una cartaAlAzar dentro de las posibles cartas y la borra de las posibles
	GameObject cartaAlAzar(){
		GameObject carta = new GameObject ();
		SpriteRenderer spriteRender = carta.AddComponent<SpriteRenderer>();
		BoxCollider2D coll = carta.AddComponent<BoxCollider2D>();
		coll.size = new Vector2(1.4f, 1.9f);
		Sprite azar = posiblesCartas [Random.Range (0, posiblesCartas.Count)];
		spriteRender.sprite = azar;
		carta.name = azar.name;
		carta.tag = "carta";
		posiblesCartas.Remove (azar);
		return carta;
	}

	void vueltaCarta(){
		GameObject vueltaCarta = new GameObject();
		vueltaCarta.name = "Vuelta";
		SpriteRenderer spriteRender = vueltaCarta.AddComponent<SpriteRenderer>();
		BoxCollider2D coll = vueltaCarta.AddComponent<BoxCollider2D>();
		coll.size = new Vector2(1.4f, 1.9f);
		spriteRender.sprite = vueltaCartaSprite;
		vueltaCarta.tag = "vuelta";
		vueltaCarta.transform.position = new Vector3(posicionInicial.x,
		                                             posicionInicial.y + 0.01f * (dorsos.Count - 1),
		                                             1);
	}
	void vuelta(){
		indiceCartaADestruir = 0;
		limpiarCartas();
		limpiarDorsos();
		for (int i = 0; i < cartas.Count - 1; i++) {
			GameObject dorso = new GameObject();
			dorso.name = "Dorso" + i;
			SpriteRenderer spriteRender = dorso.AddComponent<SpriteRenderer>();
			BoxCollider2D coll = dorso.AddComponent<BoxCollider2D>();
			coll.size = new Vector2(1.4f, 1.9f);
			spriteRender.sprite = dorsoSprite;
			dorso.tag = "dorso";
			dorso.transform.position = new Vector2(posicionInicial.x, posicionInicial.y + 0.01f * i);
			dorsos.Add(dorso);
		}
		indiceCartaADestruir = 0;
		Destroy (GameObject.Find ("Vuelta"));
	}
	void limpiarDorsos(){
		dorsos.Clear();
	}
	void limpiarCartas(){
		for (int i = 0; i < cartas.Count; i++) {
			cartas[i].SetActive(false);
		}
	}	

	void creacionMazos(){

		for (int i = 0; i < 7; i++) {
			for(int j = 0; j < 7; j++){
				if(i > j){
					GameObject cartaMontonDorso = cartaAlAzar ();
					cartaMontonDorso.name = "Monton" + (i+1) + (j+1);
					cartaMontonDorso.tag = "dorsoMonton";
					spritesMontones[i].Add(cartaMontonDorso.GetComponent<SpriteRenderer>().sprite);
					cartaMontonDorso.GetComponent<SpriteRenderer> ().sprite = dorsoSprite;
					cartaMontonDorso.transform.position = new Vector3 (posicionMontones[i].x,
					                                                   posicionMontones[i].y -0.25f * j,
					                                                   -1 * j);
					montones[i].Add(cartaMontonDorso);

				} else if(i == j){
					GameObject cartaGiradaMonton = cartaAlAzar ();
					cartaGiradaMonton.name = cartaGiradaMonton.GetComponent<SpriteRenderer> ().sprite.name;
					cartaGiradaMonton.tag = "monton" + (i + 1);
					cartaGiradaMonton.transform.position = new Vector3 (posicionMontones[i].x,
					                                                    posicionMontones[i].y -0.25f * j,
					                                                    -1 * j);
					montones[i].Add(cartaGiradaMonton);

				}
			}		
		}

	}

	void siguientePica(GameObject carta, int posicion){
		carta.transform.position = new Vector3 (posicionMazo.x + 4f, posicionMazo.y, -posicion);
		carta.tag = "montonPicas";
		cartas.Remove(carta);
		montonPicas.Add (carta);
		//indiceCartaASacar--;
		picas[posicion] = true;
		for (int i = 0; i < 7; i++) {
			montones[i].Remove(carta);	
			if(sePuedePulsar(i)){
				montones[i][montones[i].Count - 1].tag = "dorsoMonton" + (i + 1);
			}
		}

	}

	void siguienteTrebol(GameObject carta, int posicion){
		carta.transform.position = new Vector3 (posicionMazo.x + 6f, posicionMazo.y, -posicion);
		carta.tag = "montonTreboles";
		cartas.Remove(carta);
		montonTreboles.Add (carta);
		//indiceCartaASacar--;
		treboles[posicion] = true;
		for (int i = 0; i < 7; i++) {
			montones[i].Remove(carta);
			if(sePuedePulsar(i)){
				montones[i][montones[i].Count - 1].tag = "dorsoMonton" + (i + 1);
			}
		}
		
	}

	void siguienteCorazon(GameObject carta, int posicion){
		carta.transform.position = new Vector3 (posicionMazo.x + 8f, posicionMazo.y, -posicion);
		carta.tag = "montonCorazones";
		cartas.Remove(carta);
		montonCorazones.Add (carta);
		//indiceCartaASacar--;
		corazones[posicion] = true;
		for (int i = 0; i < 7; i++) {
			montones[i].Remove(carta);
			if(sePuedePulsar(i)){
				montones[i][montones[i].Count - 1].tag = "dorsoMonton" + (i + 1);
			}
		}
		
	}

	void siguienteDiamante(GameObject carta, int posicion){
		carta.transform.position = new Vector3 (posicionMazo.x + 10f, posicionMazo.y, -posicion);
		carta.tag = "montonDiamantes";
		cartas.Remove(carta);
		montonDiamantes.Add (carta);
		//indiceCartaASacar--;
		diamantes[posicion] = true;
		for (int i = 0; i < 7; i++) {
			montones[i].Remove(carta);
			if(sePuedePulsar(i)){
				montones[i][montones[i].Count - 1].tag = "dorsoMonton" + (i + 1);
			}
		}
		
	}

	void vueltaMonton(GameObject carta, int monton){
		Debug.Log (spritesMontones [monton].Count);
		carta.GetComponent<SpriteRenderer> ().sprite = spritesMontones[monton][spritesMontones[monton].Count - 1];
		carta.name = carta.GetComponent<SpriteRenderer> ().sprite.name;
		carta.tag = "monton" + (monton + 1);
		spritesMontones [monton].Remove (carta.GetComponent<SpriteRenderer> ().sprite);

	}

	bool sePuedePulsar(int monton){
		for (int i = 0; i < montones[monton].Count; i++) {
			if(montones[monton][i].name.Contains("card")){
				return false;
			} 		
		}
		return true;
	}
}

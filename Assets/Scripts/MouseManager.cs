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

	List<GameObject>[] montones = new List<GameObject>[7];
	Vector2[] posicionMontones = new Vector2[7];

	int indiceCartaADestruir = 0;
	int indiceCartaASacar = 0;
	bool primeraVuelta = false;
	GameObject cartaCogida;
	
	// Use this for initialization
	void Start () {
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
				if(hit.collider.gameObject.tag.Equals("dorso")){
					Destroy(dorsos[indiceCartaADestruir]);
					indiceCartaADestruir++;
					if(primeraVuelta == false){
						GameObject carta;
						carta = cartaAlAzar();
						carta.transform.position = posicionMazo;
						cartas.Add(carta);
					} 
					else {

						cartas[indiceCartaASacar].SetActive(true);
						indiceCartaASacar--;

					}
					if(indiceCartaADestruir == dorsos.Count){
						vueltaCarta();
						cartas.Reverse();
						primeraVuelta = true;
						indiceCartaASacar = cartas.Count - 1;
					}
				}
				if(hit.collider.gameObject.tag.Equals("vuelta")){
					vuelta();
				}
				if(hit.collider.gameObject.tag.Equals("carta")){
					cartaCogida = hit.collider.gameObject;
				}
				if(hit.collider.gameObject.tag.Equals("monton1")){
					cartaCogida = hit.collider.gameObject;
				}
				if(hit.collider.gameObject.tag.Equals("monton2")){
					cartaCogida = hit.collider.gameObject;
				}
				if(hit.collider.gameObject.tag.Equals("monton3")){
					cartaCogida = hit.collider.gameObject;
				}
				if(hit.collider.gameObject.tag.Equals("monton4")){
					cartaCogida = hit.collider.gameObject;
				}
				if(hit.collider.gameObject.tag.Equals("monton5")){
					cartaCogida = hit.collider.gameObject;
				}
				if(hit.collider.gameObject.tag.Equals("monton6")){
					cartaCogida = hit.collider.gameObject;
				}
				if(hit.collider.gameObject.tag.Equals("monton7")){
					cartaCogida = hit.collider.gameObject;
				}
			}
		} 

		if (Input.GetMouseButtonUp(0) && cartaCogida != null) {
			if(cartaCogida.tag.Equals("carta")){
				cartaCogida.transform.position = posicionMazo;
				cartaCogida = null;
			}
			else if(cartaCogida.tag.Equals("monton1")){
				cartaCogida.transform.position = primerMonton;
				cartaCogida = null;
			}
			else if(cartaCogida.tag.Equals("monton2")){
				cartaCogida.transform.position = new Vector3(segundoMonton.x, segundoMonton.y - 0.25f,-2);
				cartaCogida = null;
			}
			else if(cartaCogida.tag.Equals("monton3")){
				cartaCogida.transform.position = new Vector3(tercerMonton.x, tercerMonton.y - 0.5f,-3);
				cartaCogida = null;
			}
			else if(cartaCogida.tag.Equals("monton4")){
				cartaCogida.transform.position = new Vector3 (cuartoMonton.x, cuartoMonton.y - 0.75f,-4);
				cartaCogida = null;
			}
			else if(cartaCogida.tag.Equals("monton5")){
				cartaCogida.transform.position = new Vector3 (quintoMonton.x, quintoMonton.y - 1f,-5);
				cartaCogida = null;
			}
			else if(cartaCogida.tag.Equals("monton6")){
				cartaCogida.transform.position = new Vector3 (sextoMonton.x, quintoMonton.y - 1.25f,-6);
				cartaCogida = null;
			}
			else if(cartaCogida.tag.Equals("monton7")){
				cartaCogida.transform.position = new Vector3 (septimoMonton.x, septimoMonton.y - 1.5f,-7);
				cartaCogida = null;
			}

		}
	}

	void FixedUpdate(){
		if (cartaCogida != null) {
			Vector3 mouseWorld3d = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector3 mouse2dPos = new Vector3(mouseWorld3d.x, mouseWorld3d.y,-9);

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
		vueltaCarta.transform.position = new Vector2(posicionInicial.x,
		                                             posicionInicial.y + 0.01f * (dorsos.Count - 1));
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
	}
	void limpiarDorsos(){
		dorsos.Clear();
	}
	void limpiarCartas(){
		for (int i = 0; i < cartas.Count; i++) {
			//Destroy (cartas[i]);
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
					//GUARDAR EL SPRITE!!!!
					cartaMontonDorso.GetComponent<SpriteRenderer> ().sprite = dorsoSprite;
					cartaMontonDorso.transform.position = new Vector3 (posicionMontones[i].x,
					                                                   posicionMontones[i].y -0.25f * j,
					                                                   -1 * j);
					montones[i].Add(cartaMontonDorso);

				} else if(i == j){
					GameObject cartaGiradaMonton = cartaAlAzar ();
					cartaGiradaMonton.name = "Monton" + (i + 1) + (j+1);
					cartaGiradaMonton.tag = "monton" + (i + 1);
					cartaGiradaMonton.transform.position = new Vector3 (posicionMontones[i].x,
					                                                    posicionMontones[i].y -0.25f * j,
					                                                    -1 * j);
					montones[i].Add(cartaGiradaMonton);

				}
			}		
		}

	}
}

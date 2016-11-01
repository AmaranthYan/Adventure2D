using UnityEngine;
using System.Collections;
using System.Linq;

public class CharacterSwitch : MonoBehaviour {
	private int currentCharacter = 0;
	[SerializeField]
	private GameObject[] characters;

	void Awake() {
		GameObject.DontDestroyOnLoad(this);
	}

	void Start() {
	
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.C))
			Switch();
	}

	public GameObject GetCurrentCharacter() {
		return characters[currentCharacter];
	}

	public bool AddCharacter(GameObject character) {
        if (characters.Contains(character))
        {
            return false;
        }
		for (int i = 0; i < characters.Length; i++) {
			if (characters[i] == null) {
				character.transform.parent = this.transform;
				characters[i] = character;
				return true;
			}
		}
		return false;
	}

	public bool RemoveCharacter(GameObject character) {
		for (int i = 0; i < characters.Length; i++) {
			if (characters[i].Equals(character)) {
				if ((i == currentCharacter) && (i == Switch()))
				    return false;
				characters[i].transform.parent = null;
				characters[i] = null;
				return true;
			}
		}
		return false;
	}

	public int Switch() {
		if (!characters[currentCharacter].GetComponent<CharacterController2D>().GroundCheck())
			return currentCharacter;
		this.GetComponent<ScriptController>().DisableAbility(characters[currentCharacter]);
		this.GetComponent<ScriptController>().DisableCharacterController(characters[currentCharacter]);
		characters[currentCharacter].GetComponent<CharacterController2D>().Halt();
		(characters[currentCharacter].GetComponent<Renderer>() as SpriteRenderer).sortingLayerName = "Suspended";
		do {
			currentCharacter++;
			if (currentCharacter >= characters.Length)
				currentCharacter = 0;
		} while(characters[currentCharacter] == null);
		(characters[currentCharacter].GetComponent<Renderer>() as SpriteRenderer).sortingLayerName = "Selected";
		characters[currentCharacter].GetComponent<Animator>().SetTrigger("focus");
		this.GetComponent<ScriptController>().EnableCharacterController(characters[currentCharacter]);
		this.GetComponent<ScriptController>().EnableAbility(characters[currentCharacter]);
		return currentCharacter;
	}
}
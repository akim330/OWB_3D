
using UnityEngine;
using TMPro;

public class DialogueBoxOLD : MonoBehaviour
{
    //private int npcID;

    //[SerializeField] private float timeVisible = 5f;
    //private float timer;

    //private string _line;
    //public string line
    //{
    //    get { return _line; }
    //    set
    //    {
    //        _line = value;
    //        textMesh.text = value;
    //        textMesh.ForceMeshUpdate();

    //        SetBox(textMesh.bounds.size.x, textMesh.bounds.size.y);

    //        ShowBox();

    //        timer = timeVisible;
    //    }
    //}

    //private TextMeshPro textMesh;
    //private RectTransform textMeshTransform;
    //private SpriteRenderer _renderer;

    //private Color disabledColor = new Color(1, 1, 1, 0);

    //private bool _talking;

    //// Set according to preference
    //[SerializeField] private float dialogueBorder = 0.2f;

    //// This is based on the amount of space in the sprite (8 px which is 1 unit)
    //private float spaceBelowBubble = 1f;

    //[SerializeField] SpriteRenderer _talkableBubbleRenderer;

    //private DialogueInitiator _currentPartner;
    //private Dialogue _dialogue;
    //private int _currentLineNumber;

    //[Header("Debug")]
    //[TextArea] public string startingDialogue;

    //private void OnValidate()
    //{
    //    textMesh = GetComponentInChildren<TextMeshPro>();
    //    textMeshTransform = textMesh.GetComponent<RectTransform>();
    //    _renderer = GetComponent<SpriteRenderer>();

    //    //line = startingDialogue;
    //}

    //private void Awake()
    //{
    //    textMesh = GetComponentInChildren<TextMeshPro>();
    //    textMeshTransform = textMesh.GetComponent<RectTransform>();
    //    _renderer = GetComponent<SpriteRenderer>();

    //    HideBox();
    //    HideTalkableBubble();
    //}

    //// Start is called before the first frame update
    //void Start()
    //{
    //    npcID = GetComponentInParent<NPC>().id;
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (timer > 0)
    //    {
    //        timer -= Time.deltaTime;

    //        if (timer <= 0)
    //        {
    //            EndConversation();
    //            //ShowTalkableBubble();
    //        }
    //    }
    //}

    //private void HideBox()
    //{
    //    _renderer.color = disabledColor;
    //    textMesh.color = disabledColor;
    //}

    //private void ShowBox()
    //{
    //    _renderer.color = Color.white;
    //    textMesh.color = Color.black;
    //}

    //private void SetBox(float textWidth, float textHeight)
    //{
    //    //Debug.Log($"Setting width: {textWidth}, height: {textHeight}");

    //    var endX = -0.5f;
    //    var startX = endX - textWidth - 2 * dialogueBorder;
    //    var startY = 3f;
    //    var endY = startY + textHeight + 2 * dialogueBorder;

    //    transform.localPosition = new Vector2((startX + endX) / 2, (startY - 1f + endY) / 2);

    //    var boxWidth = endX - startX;
    //    var boxHeight = endY - startY + 1.9f;
    //    _renderer.size = new Vector2(boxWidth, boxHeight);

    //    textMeshTransform.sizeDelta = new Vector2(textWidth, textHeight);
    //    textMeshTransform.localPosition = new Vector3(0, spaceBelowBubble / 2, -1f);
    //}

    //public void ShowTalkableBubble()
    //{
    //    if (!_talking)
    //    {
    //        //Debug.Log($"{transform.parent.name}: Showing bubble");

    //        _talkableBubbleRenderer.color = Color.white;
    //    }
    //}

    //public void HideTalkableBubble()
    //{
    //    //Debug.Log($"{transform.parent.name}: Hiding bubble");

    //    _talkableBubbleRenderer.color = disabledColor;

    //}

    //public void StartConversation(DialogueInitiator other)
    //{
    //    HideTalkableBubble();
    //    _currentPartner = other;
    //    _dialogue = Managers.Dialogue.GetDialogue(npcID);

    //    if (_dialogue.lines.Length == 0)
    //    {
    //        Debug.LogError("ERROR: Got dialogue with 0 lines");
    //    }
    //    else
    //    {
    //        _currentLineNumber = 0;
    //        line = _dialogue.lines[0];
    //        _talking = true;
    //    }
    //}

    //public void EndConversation()
    //{
    //    HideBox();
    //    if (_currentPartner != null)
    //    {
    //        _currentPartner.OnConversationEnd();
    //    }
    //    _talking = false;
    //}

    //public void ProgressConversation()
    //{
    //    if (_currentLineNumber == _dialogue.lines.Length - 1)
    //    {
    //        EndConversation();
    //    }
    //    else
    //    {
    //        _currentLineNumber++;
    //        line = _dialogue.lines[_currentLineNumber];
    //    }
    //}
}

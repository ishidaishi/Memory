using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsDirector : MonoBehaviour
{
    [SerializeField] List<GameObject> prefabSpades;
    [SerializeField] List<GameObject> prefabClubs;
    [SerializeField] List<GameObject> prefabDiamonds;
    [SerializeField] List<GameObject> prefabHearts;
    [SerializeField] List<GameObject> prefabJokers;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //�V���b�t�������J�[�h��Ԃ�
    public List<CardController> GetShuffleCards()
    {
      List<CardController> ret = new List<CardController>();
      
      ret.AddRange(createCards(SuitType.Spade));
      ret.AddRange(createCards(SuitType.Club));
      ret.AddRange(createCards(SuitType.Diamond));
      ret.AddRange(createCards(SuitType.Heart));
      
      ShuffleCards(ret);
      
      return ret;
    }
    
     //�_�o����Ŏg���J�[�h��Ԃ�
    public List<CardController> GetMemoryCards()
    {
      List<CardController> ret = new List<CardController>();
      
      ret.AddRange(createCards(SuitType.Spade,10));
      ret.AddRange(createCards(SuitType.Club,10));

      ShuffleCards(ret);
      
      return ret;
    }
    
    //�V���b�t��
    public void ShuffleCards(List<CardController>cards)
    {
       for(int i=0;i<cards.Count;i++)
       {
         int rnd =Random.Range(0, cards.Count);
         CardController tmp = cards[i];
         
         cards[i]=cards[rnd];
         cards[rnd]=tmp;
       }
    }
    
    //�J�[�h�쐬
    List<CardController> createCards(SuitType suittype,int count = -1)
    {
      List<CardController> ret = new List<CardController>();
      
      List<GameObject> prefabcards = prefabSpades;
      Color suitcolor =Color.black;
      
      if(SuitType.Club ==suittype)
      {
       prefabcards =prefabClubs;  
      }
      else if(SuitType.Diamond ==suittype)
      {
       prefabcards =prefabDiamonds;
       suitcolor =Color.red;  
      }
      else if(SuitType.Heart ==suittype)
      {
       prefabcards =prefabHearts;
       suitcolor =Color.red;  
      }
      
      if(0>count)
      {
        count=prefabcards.Count;
      }
      
      
      //�J�[�h����
      for(int i=0;i<count;i++)
      {
        GameObject obj=Instantiate(prefabcards[i]);
        
       �@BoxCollider bc=obj.AddComponent<BoxCollider>();
       
       Rigidbody rb=obj.AddComponent<Rigidbody>();
       
       bc.isTrigger=true;
       rb.isKinematic=true;
       
       CardController ctrl=obj.AddComponent<CardController>();
       
       ctrl.Suit=suittype;
       ctrl.SuitColor =suitcolor;
       ctrl.PlayerNo=-1;
       ctrl.No=i+1;
       
       ret.Add(ctrl);
       
      }

      return ret;
      
     }
}

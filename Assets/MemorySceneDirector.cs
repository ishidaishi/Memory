using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemorySceneDirector : MonoBehaviour
{
    [SerializeField] CardsDirector cardsDirector;
    
    [SerializeField] Text textTimer;
    
    List<CardController> cards;

    int activecount=0;
    
    int width =5;
    int height =4;
    
     List<CardController> selectCards;
     int selectCountMax = 2;
     
     bool isGameEnd;
     
     float gameTimer;
     
     int oldSecond;
     
     
     // Start is called before the first frame update
     void Start()
     {
       cards = cardsDirector.GetMemoryCards();
       
       Vector2 offset =new Vector2((width-1)/2.0f,(height-1)/2.0f);
       
       if(cards.Count <width * height)
       {
         Debug.LogError("�J�[�h������܂���");
       }
       
       for(int i=0;i<width*height;i++)
       {
         float x =(i%width - offset.x)*CardController.Width;
         float y =(i/width - offset.y)*CardController.Height;
         
         cards[i].transform.position=new Vector3(x,0,y);
         cards[i].FlipCard(false);
       }
       selectCards=new List<CardController>();
       oldSecond=-1;
     }

    // Update is called once per frame
    void Update()
    {
        if(isGameEnd) return;
        
        gameTimer+= Time.deltaTime;
        
        textTimer.text=getTimerText(gameTimer);
        
        if(Input.GetMouseButtonUp(0))
        {
           //3��ڂ̃^�b�v
           if(!canOpen()) return;
           
           Ray ray =Camera.main.ScreenPointToRay(Input.mousePosition);
           if(Physics.Raycast(ray,out RaycastHit hit))
           {
              CardController card = hit.collider.gameObject.GetComponent<CardController>();
              
              if(!card || selectCards.Contains(card)) return;
              
              card.FlipCard();
              
              selectCards.Add(card);
           }
           
        }
    }
    
    //�^�C�}�[��\������
    string getTimerText(float timer)
    {
       int sec=(int)timer%60;
       string ret =textTimer.text;
       
       if(oldSecond !=sec)
       {
           int min=(int)timer/60;
           string pmin =string.Format("{0:D2}",min);
           string psec =string.Format("{0:D2}",sec);
           
           ret= pmin + ":" + psec;
           oldSecond=sec;
       }
       return ret;
        
    }    
    //�����ꖇ�߂���邩�ǂ���
       bool canOpen()
       {
           //�񖇂�����Ă��Ȃ��ꍇ�̓J�[�h���߂��邱�Ƃ��ł���
         if(selectCards.Count < selectCountMax) return true;
         
         //�񖇂�����Ă���ꍇ�́A�I�������J�[�h�������������ǂ����`�F�b�N
         bool equal = true;
         foreach(var item in selectCards)
        {
         //�����������ǂ����`�F�b�N
         if(item.No != selectCards[0].No)
         {
           equal=false;
           //������Ă��Ȃ��J�[�h�𗠕Ԃ��ɂ���   �@
         item.FlipCard(false);
         selectCards[0].FlipCard(false);
         }
        }
        //�S������������������J�[�h�����̂܂܂ɂ���(������)
        if(equal)
        {
            //��������������񐔂��J�E���g
            activecount++;
            //�߂������J�[�h�͂��̂܂܂�(������)
           /*foreach(var item in selectCards)
           {
             item.gameObject.SetActive(false);
           }
           */

           //���ׂẴJ�[�h���߂���ꂽ��Q�[���I��
           isGameEnd = false;
           /*foreach(var item in cards)
           {
             if(item.gameObject.activeSelf)
             {
               isGameEnd =false;
               break;
             }
           }
           */

           if(activecount==(width*height)/2)
           {
               isGameEnd =true;
           }

           if(isGameEnd)
           {
             textTimer.text="�N���A!!" +getTimerText(gameTimer);
           }
        }
       
       
       selectCards.Clear();
       
       return false;
       
    }
}

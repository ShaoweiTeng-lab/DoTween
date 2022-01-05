using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
using UnityEngine.UI;
public class ShapeMove : MonoBehaviour
{
    //https://www.gushiciku.cn/pl/grfh/zh-tw
    //https://www.youtube.com/watch?v=Y8cv-rF5j6c&t=121s
    //http://dotween.demigiant.com/documentation.php#globalSettings
    //https://www.youtube.com/watch?v=jbYXTLcgmYQ
    //https://zhuanlan.zhihu.com/p/161106076
    public GameObject inShape;
    public GameObject MoveShape2;
    public Text WordTesxt;

    public Button TweenControl;
    public Tween Tweener;//可用來控制tween(所有物件) 


    public Button m_SequenceControl;
    public Sequence m_Sequence;//集合，序列
    public Sequence m_Sequence2;
    public Image Image0;
    public Image Image1;
    // Start is called before the first frame update
    void Start()
    {
        #region 物體移動
        //transform.DOMove(new Vector3(10, 0, 0), 3);
        //transform.DOMove(new Vector3(10, 0, 0), 3).SetEase(Ease.InOutCubic).SetLoops(-1,LoopType.Yoyo);//-1代表loop次數無限
        //transform.DORotate(new Vector3(0, 360, 0),1.5f,RotateMode.LocalAxisAdd).SetLoops(-1, LoopType.Restart);
        //inShape.transform.DOLocalMove(new Vector3(0, 3,0 ), 1.5f).SetEase(Ease.InOutCubic).SetLoops(-1, LoopType.Yoyo);//設定本地移動(跟隨父物件)
        #endregion
        #region 完成後執行
        //transform.DOMove(new Vector3(10, 0, 0), 3).SetEase(Ease.InOutCubic).OnComplete(() => { Debug.Log("FinishMove"); transform.DOScale(new Vector3(1f, 1f, 1f), 1); });//-1代表loop次數無限 
        //inShape.transform.DOLocalMove(new Vector3(0, 3, 0), 1.5f).SetEase(Ease.InOutCubic).SetLoops(-1, LoopType.Yoyo);//設定本地移動(跟隨父物件)
        #endregion
        #region 更改文字
        //WordTesxt.DOText("<color=blue>Hello</color> World", 1, true, ScrambleMode.Lowercase);
        #endregion
        #region 多物件執行函式
        //ManyMoveto(MoveShape2.transform);
        //ManyMoveto(transform);
        #endregion
        #region CallBcak On開頭
        //Tweener = transform.DOMove(new Vector3(10, 0, 0), 3).OnComplete(() => { TweenControl.interactable = false; }) ;
        //Tweener = transform.DOMove(new Vector3(10, 0, 0), 3).OnUpdate(() => Debug.Log("Hello world")); ;
        #endregion
        #region 暫停
        //Tween 可用於 控制 所以撥放之物件 因其屬性為 tween
        TweenControl.onClick.AddListener(() =>
        {   //Play + Pause
            //if (Tweener.IsPlaying())
            //    Tweener.Pause();
            //else
            //    Tweener.Play();


            //restart 
            //Tweener.Restart();
            //倒帶
            //Tweener.Rewind();

        });
        m_SequenceControl.onClick.AddListener(() => {


            m_Sequence = SequenceShow(Image0.GetComponent<RectTransform>());
            m_Sequence2 = SequenceShow2(Image1.GetComponent<RectTransform>());
            });
        
        #endregion


    } 
    public void ManyMoveto(Transform objectdata) {
        DOTween.To(()=> objectdata.localPosition,x => objectdata.localPosition=x, new Vector3(10, 0, 0),3); 
    }

    #region Sequence 
    public Sequence SequenceShow(RectTransform rec)
    {
        #region Append
        //Append 排隊 (添加 tween) 
        Sequence seq = DOTween.Sequence();//創集合
        return seq.Append(rec.DOLocalMoveX(300, 1))//新增動作
                  .Append(rec.DOLocalMove(Vector3.zero, 1))
                  .Append(rec.DOScale(2, 1)).SetEase(Ease.InBounce).SetLoops(2, LoopType.Yoyo)
                  .Append(rec.DORotate(new Vector3(0, 360, 0) * 3, 1, RotateMode.FastBeyond360))
                  .Join(rec.GetComponent<Image>().DOColor(Color.black, 1)) //join  執行在上個動作時同時執行此動作 
                  .AppendCallback(() => Debug.Log("Hi"))
                  .OnComplete(()=>Debug.Log("Finish Func"))
                  .PrependInterval(2);//此 func 延遲幾秒執行
       
        #endregion
        #region Insert
        //   Insert(插入)  延遲幾 秒動作   
        //Sequence seq2 = DOTween.Sequence().Append(rec.DOLocalMoveX(300, 5)).Insert(1, rec.GetComponent<Image>().DOColor(Color.black, 1)) 
        //                .Append(rec.DORotate(new Vector3(0, 360, 0) * 3, 1, RotateMode.FastBeyond360));
        //return seq2;
        #endregion
    }

    public Sequence SequenceShow2(RectTransform rec)
    {
        #region Append
        //Append 排隊 (添加 tween) 
        Sequence seq = DOTween.Sequence();
        return seq.Append(rec.DOLocalMoveX(300, 1))//新增動作
                  .Append(rec.DORotate(new Vector3(0, 360, 0) * 3, 1, RotateMode.FastBeyond360))
                  .Append(rec.GetComponent<Image>().DOColor(Color.red, 1)).SetEase(Ease.InBounce).SetLoops(2, LoopType.Yoyo);
        //join 同時執行上個動作
        #endregion 
    }
    #endregion
}

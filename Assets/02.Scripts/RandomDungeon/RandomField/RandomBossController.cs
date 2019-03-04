using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBossController : MonoBehaviour {

    // 기본 wall은 Field에서 생성
    // 여기선 각 boss의 테마에 맞게 Wall을 덮어씌우거나 값 변경
    // 각 보스Field 데이터 관리 

    public GameObject[] bossLandforms;   // Boss 오브젝트도 Landform안에 들어가있게.  
                                         // 자신이 가진 BossController를 통해 잠시 후 등장 애니메이션 재생을 통해 등장
    private Transform[] walls;

    public static RandomBossController instance;

    private void Awake()
    {
        instance = this;
    }


    public void RandomBoss(Field field)
    {
        walls = field.walls;

        int ran = Random.Range(0, bossLandforms.Length);
        SettingTheme(ran);

        // 이때의 Landform의 SortLayer는 wall보다 높아야 한다. 
        GameObject bossLandform = Instantiate(bossLandforms[ran]);
        bossLandform.transform.SetParent(field.transform);
        bossLandform.transform.position = Vector2.zero;
    }


    private void SettingTheme(int ran)
    {
        switch(ran)
        {
            case 1:
                KnightTheme();
                break;
        }
    }

    private void KnightTheme()
    {
        walls[0].localPosition += new Vector3(-13, 0, 0);
        walls[1].localPosition += new Vector3(13, 0, 0);
        walls[2].localPosition += new Vector3(-8, 0, 0);
        walls[3].localPosition += new Vector3(8, 0, 0);
        
        // wall의 각 기본 통로 위치에 grating(쇠창살) 생성 (이때 grating의 SortLayer는 wall보다 낮아야한다.)
        // wall의 색상및 주변 환경 변경 
    }
}

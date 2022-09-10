using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SliderScript : MonoBehaviour
{

    private Slider _slider;

    [SerializeField]
    private LifeGame _lifeGame;

    float _maxSp;
    float _nowSp;

    // Use this for initialization
    void Start()
    {

        _slider = GetComponent<Slider>();

        _lifeGame = _lifeGame.gameObject.GetComponent<LifeGame>();

        _maxSp = _lifeGame._maxSpeed;
        //�X���C�_�[�̍ő�l�̐ݒ�
        _slider.maxValue = _maxSp;

        _nowSp = _lifeGame._speed;
        //�X���C�_�[�̌��ݒl�̐ݒ�
        _slider.value = _nowSp;

        _slider.value = _nowSp;
    }


    public void OnSlider()
    {
        _nowSp =_slider.value;
         _lifeGame._speed = _slider.value;
    }

}
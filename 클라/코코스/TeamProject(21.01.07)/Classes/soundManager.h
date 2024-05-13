#pragma once
#include <cocos2d.h>
USING_NS_CC;

#include "ui/CocosGUI.h"
using namespace ui;

class soundManager :public Layer
{
public:
	bool init();

	static soundManager* getIns();

	void btnClk(Ref* ref, CheckBox::EventType type), sliderMove1(Ref* ref, Slider::EventType type), sliderMove2(Ref* ref, Slider::EventType type), chkBt(Ref* ref, CheckBox::EventType type), bgm(int i), sfx(int i), stopA();

	CREATE_FUNC(soundManager);
};
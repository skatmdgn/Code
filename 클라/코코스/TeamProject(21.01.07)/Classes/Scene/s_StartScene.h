#pragma once
#pragma execution_character_set("utf-8")
#include <cocos2d.h>
USING_NS_CC;
using namespace std;
#include <ui/CocosGUI.h>
using namespace ui;
class s_StartScene :public Scene
{
public:
	bool init();
	CREATE_FUNC(s_StartScene);

	void onKeyPress(EventKeyboard::KeyCode key, Event* e);
	void rabel();
	void btnclk(Ref* ref, Widget::TouchEventType type);
};


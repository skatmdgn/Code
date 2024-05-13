#pragma once
#pragma execution_character_set("utf-8")
#include <cocos2d.h>
USING_NS_CC;

class s_HowScene:public Scene
{
public:
	bool init();
	CREATE_FUNC(s_HowScene);

	void onPress(EventKeyboard::KeyCode key, Event* e);
	void rabel();
};


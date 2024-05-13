#pragma once
#pragma execution_character_set("utf-8")
#include <cocos2d.h>
USING_NS_CC;
using namespace std;
class s_GameoverScene:public Scene
{
public:
	bool init();
	CREATE_FUNC(s_GameoverScene);

	bool nowBlk;
	
	void onKeyPress(EventKeyboard::KeyCode key, Event* e);
	void next();
};


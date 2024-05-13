#pragma once
#include <cocos2d.h>
USING_NS_CC;
class s_GameOverScene :public Scene
{
public:
	bool init();
	CREATE_FUNC(s_GameOverScene);

	void OnkeyboardPress(EventKeyboard::KeyCode key, Event* e);
};


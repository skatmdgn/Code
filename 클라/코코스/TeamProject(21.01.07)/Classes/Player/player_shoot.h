#pragma once
#include <cocos2d.h>
USING_NS_CC;
class player_shoot:public Layer
{
public:
	bool init(), onPause = false;
	CREATE_FUNC(player_shoot);

	void mouseDown(EventMouse* e);

	static player_shoot* getIns();
};


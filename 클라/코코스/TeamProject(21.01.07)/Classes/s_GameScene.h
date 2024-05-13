#pragma once
#include <cocos2d.h>
USING_NS_CC;
class s_GameScene :public Scene
{
public :
	bool init();
	CREATE_FUNC(s_GameScene);
	
	float Time=0;
	bool bossTrue;

	void update(float dt);
};


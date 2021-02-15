#pragma once
#include <cocos2d.h>
USING_NS_CC;

class GameOver : public Scene
{
public:
	bool init();
	CREATE_FUNC(GameOver);

	float timer = 0;

	void update(float dt);
};


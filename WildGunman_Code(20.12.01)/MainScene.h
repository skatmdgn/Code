#pragma once
#include <cocos2d.h>
USING_NS_CC;
using namespace std;

class MainScene : public Scene
{
public:
	bool init(), tim = false, doorOpen = true, touchBegan(Touch* t, Event* e);
	CREATE_FUNC(MainScene);

	void update(float dt), sprOpen(int x), sprClose(int x), reGame();

	int doorRan = rand() % 5, scorenum = 0;
	float monatk = (rand() / (float)RAND_MAX) / 2 + 0.5, yuatk = 0;
	Label* yu;
	string st;
};


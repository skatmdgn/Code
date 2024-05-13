#pragma once
#include <cocos2d.h>
USING_NS_CC;
class Ui_Score:public Layer
{
public:
	bool init();

	int finalScore;
	bool checkLabel;

	void scoreUp(int score);
	void update(float dt);

	CREATE_FUNC(Ui_Score);

	void reset();
	
	static Ui_Score* getIns();
};


#pragma once
#include <cocos2d.h>
USING_NS_CC;
class Ui_hpicon:public Layer
{
public:
	bool init();
	void damage();

	CREATE_FUNC(Ui_hpicon);

	void reset();

	static Ui_hpicon* getIns();
};


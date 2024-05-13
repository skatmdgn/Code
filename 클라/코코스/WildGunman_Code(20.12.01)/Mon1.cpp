#include "Mon1.h"

bool Mon1::init()
{
	Sprite* mon = Sprite::create("mon1.png");
	this->addChild(mon);
	mon->setName("mon");
	return true;
}

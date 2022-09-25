#include "UpDoor.h"

bool UpDoor::init()
{
	Sprite* ud = Sprite::create("door_up.png");
	this->addChild(ud);
	ud->setName("up");

	return true;
}

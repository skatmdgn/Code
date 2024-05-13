#include "DownDoor.h"

bool DownDoor::init()
{
	Sprite* dd = Sprite::create("door_down.png");
	this->addChild(dd);
	dd->setName("down");

	return true;
}

#include "Ui_hpicon.h"
#include "../Player/Player.h"

static Ui_hpicon* obj = 0;

bool Ui_hpicon::init()
{
    for (int i = 1; i < 4; i++) {
        Sprite* hp = Sprite::create("hud_heartFull.png");
        this->addChild(hp);
        hp->setTag(i);
        //icon사이값은 70으로 지정
        hp->setPosition(Vec2(i*70, 680));
    }

    return true;
}

void Ui_hpicon::damage()
{
    if (Player::getIns()->hp > 0 && Player::getIns()->isDamage == true) {
        Sprite* hp = (Sprite*)this->getChildByTag(Player::getIns()->hp);
        hp->setTexture("hud_heartEmpty.png");
    }
}

void Ui_hpicon::reset()
{
    for (int i = 1; i < 4; i++) {
        Sprite* hp = (Sprite*)this->getChildByTag(i);
        hp->setTexture("hud_heartFull.png");
    }
}

Ui_hpicon* Ui_hpicon::getIns()
{
    if (obj == 0) {
        obj = Ui_hpicon::create();
        obj->retain();
    }
    return obj;
}

#include "player_shoot.h"
#include "../bulletManager.h"
#include "../laser/laser1.h"
#include "../laser/laser2.h"
#include "../ui/Ui_Score.h"
#include "soundManager.h"

static player_shoot* obj = NULL;

bool player_shoot::init()
{
    EventListenerMouse* em = EventListenerMouse::create();
    em->onMouseDown = CC_CALLBACK_1(player_shoot::mouseDown, this);
    _eventDispatcher->addEventListenerWithSceneGraphPriority(em, this);

    return true;
}

void player_shoot::mouseDown(EventMouse* e)
{
    EventMouse* em = (EventMouse*)e;
    if (em->getMouseButton() == EventMouse::MouseButton::BUTTON_LEFT) {
        soundManager::getIns()->sfx(2);
        if (onPause == false)
        {
            if ((laser1*)bulletManager::getIns()->b_p_vec1_wait.size() > 0) {
                laser1* p = (laser1*)bulletManager::getIns()->b_p_vec1_wait.back();
                bulletManager::getIns()->b_p_vec1.pushBack(p);
                bulletManager::getIns()->b_p_vec1_wait.eraseObject(p);
                p->laser1_Active();
            }
            else {
                laser1* p = laser1::create();
                this->addChild(p);
                bulletManager::getIns()->b_p_vec1.pushBack(p);
                p->laser1_Active();
                p->setZOrder(3);
            }
            if (Ui_Score::getIns()->finalScore > 3000) {
                if ((laser2*)bulletManager::getIns()->b_p_vec2_wait.size() > 0) {
                    laser2* p = (laser2*)bulletManager::getIns()->b_p_vec2_wait.back();
                    bulletManager::getIns()->b_p_vec2.pushBack(p);
                    bulletManager::getIns()->b_p_vec2_wait.eraseObject(p);
                    p->laser2_Active();
                }
                else {
                    laser2* p = laser2::create();
                    this->addChild(p);
                    bulletManager::getIns()->b_p_vec2.pushBack(p);
                    p->laser2_Active();
                    p->setZOrder(3);
                }
            }
        }
    }
}


player_shoot* player_shoot::getIns()
{
    if (obj == NULL) {
        obj = player_shoot::create();
        obj->retain();
    }
    return obj;
}

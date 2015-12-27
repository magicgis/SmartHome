#include "clockapplication.h"

using namespace std;
using namespace EmbeddedSystem::API;
using namespace EmbeddedSystem::Clock::Model;

QImage get_app_image(){
    return QImage(":/clock/images/ClockAppIcon.png");
}

string get_app_name(){
    return "Clock";
}

ClockApplication::ClockApplication() : Application(get_app_image(), get_app_name()){

}

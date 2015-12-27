#include "coresystem.h"

#include <cstddef>
#include <vector>

#include "Clock/Model/clockapplication.h"

using namespace EmbeddedSystem::API;
using namespace std;

CoreSystem* CoreSystem::s_instance;

CoreSystem* CoreSystem::get_instance(){
    return s_instance;
}

void CoreSystem::init(){
    if(s_instance != NULL)
        return;

    s_instance = new CoreSystem();
}

ApplicationManager* CoreSystem::get_application_manager()
{
    return m_application_manager;
}

vector<Application> init_applications();
CoreSystem::CoreSystem(){
    vector<Application> apps = init_applications();
    m_application_manager = new ApplicationManager(apps);
}

vector<Application> init_applications(){
    vector<Application> apps;

    apps.push_back(EmbeddedSystem::Clock::Model::ClockApplication());

    return apps;
}

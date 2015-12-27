#include "applicationmanager.h"

using namespace EmbeddedSystem::API;
using namespace std;

ApplicationManager::ApplicationManager(vector<Application> p_applications)
{
    m_applications = std::vector<Application>(p_applications);
}

std::vector<Application> ApplicationManager::get_applications()
{
    return std::vector<Application>(m_applications);
}

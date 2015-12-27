#include "application.h"

using namespace std;
using namespace EmbeddedSystem::API;

Application::Application(QImage p_image, string p_name){
    m_image = p_image;
    m_name = p_name;
}

QImage Application::get_image(){
    return m_image;
}

string Application::get_name(){
    return m_name;
}

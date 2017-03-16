from django.conf.urls import include, url
from rest_framework import routers
from KittyKrawler import views

#router = routers.DefaultRouter()
#router.register(r'save', views.SaveView)

urlpatterns = [
    url(r'^save/', views.SaveView.as_view()),
]
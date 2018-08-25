from django.conf.urls import url
from views import index, show_worlds

urlpatterns = [
    url(r'^$', index),
    url(r'^show_worlds', show_worlds),
]

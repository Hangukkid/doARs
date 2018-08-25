from django import forms
import os

FILE_PATH = os.path.join(os.path.dirname(os.path.abspath(__file__)), 'Worlds')

class UploadWorld(forms.Form):
    name = forms.CharField(max_length=50)
    world = forms.FileField()
        

def handle_uploaded_world (world_file, world_name):
    with open(os.path.join(FILE_PATH, world_name + '.world'), 'wb+') as destination:
        for chunk in world_file.chunks():
            destination.write(chunk)
import os, json

from django.shortcuts import render
from django.shortcuts import HttpResponse, Http404, HttpResponseRedirect
from django.views.decorators.csrf import csrf_exempt
from django.core.files.storage import FileSystemStorage

from forms import UploadWorld, handle_uploaded_world

# Create your views here.

FILE_PATH = os.path.dirname(os.path.abspath(__file__))
if not os.path.exists(os.path.join(FILE_PATH, "Worlds")):
    os.makedirs(os.path.join(FILE_PATH, "Worlds"))

@csrf_exempt
def index (request):
    if request.method == 'POST':
        world = UploadWorld(request.POST, request.FILES)
        if world.is_valid():
            handle_uploaded_world(request.FILES['world'], request.POST['name'])
            return HttpResponse('success')
        return HttpResponse('invalid file entry')

    elif request.method == "GET":
        world = request.META.get("HTTP_WORLD") if request.META.get("HTTP_WORLD") != None else "welp"
        file_path = os.path.join(FILE_PATH, "Worlds", world.lower() + '.world')
        if os.path.exists(file_path):
            with open (file_path, 'rb') as fh:
                response = HttpResponse(fh.read(), content_type="application/octet-stream")
                response['Content-Disposition'] = 'inline; filename=' + os.path.basename(file_path)
                return response
        return HttpResponse('Get failed.')

    return HttpResponse("That's a nono")


@csrf_exempt
def show_worlds (request):
    if request.method == "GET":
        list_of_worlds = []
        given_world = request.META.get("HTTP_WORLD") if request.META.get("HTTP_WORLD") != None else ""
        for file in os.listdir(os.path.join(FILE_PATH, "Worlds")):
            list_of_worlds.append(file[:len(file) - 6])
        return HttpResponse(json.dumps(autocomplete(list_of_worlds, given_world)))
    return HttpResponse("That's a nono")


def autocomplete (list_of_worlds, given_world):
    world_to_return = []
    for world in list_of_worlds:
        if world.startswith(given_world):
            world_to_return.append(world)
    return world_to_return
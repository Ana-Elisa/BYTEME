from KittyKrawler.models import GameSave, Leaderboard, Item
from KittyKrawler.serializers import SaveSerializer, LeaderboardSerializer, ItemSerializer
from django.http import Http404
from rest_framework.views import APIView
from rest_framework.response import Response
from rest_framework import permissions, status

from rest_framework import viewsets
from .permissions import IsAdminOrClient

class SaveView(viewsets.ModelViewSet):
    queryset = GameSave.objects.all()
    serializer_class = SaveSerializer
    permission_classes = (IsAdminOrClient,)

    def perform_create(self, serializer):
        saves = GameSave.objects.filter(user=self.request.user, current=True)
        for save in saves:
            save.current = False
            save.save()
        game_save = serializer.save(user=self.request.user)

        leaderboard_entry = Leaderboard(user=self.request.user, game_save=game_save)
        leaderboard_entry.save()

    def get_queryset(self):
        return GameSave.objects.filter(user=self.request.user, current=True)
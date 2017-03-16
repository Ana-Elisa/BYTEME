from KittyKrawler.models import GameSave, Leaderboard, Item
from KittyKrawler.serializers import SaveSerializer, LeaderboardSerializer, ItemSerializer
from django.http import Http404
from rest_framework.views import APIView
from rest_framework.response import Response
from rest_framework import permissions, status

from rest_framework import viewsets

class SaveView(viewsets.ModelViewSet):
    queryset = GameSave.objects.all()
    serializer_class = SaveSerializer
    permission_classes = (permissions.IsAuthenticated,)

    def perform_create(self, serializer):
        saves = GameSave.objects.filter(user=self.request.user, current=True)
        for save in saves:
            save.current = False
            save.save()
        serializer.save(user=self.request.user)

    def get_queryset(self):
        return GameSave.objects.filter(user=self.request.user, current=True)
"""
    def get(self, request):
        try:
            queryset = GameSave.objects.get(user=request.user)
            serializer = SaveSerializer(queryset)
            return Response(serializer.data)
        except GameSave.DoesNotExist:
            return Http404

    def post(self, request):
        serializer = SaveSerializer(data=request.data)
        if serializer.is_valid():
            serializer.save()
            return Response(serializer.data, status=status.HTTP_201_CREATED)
        return Response(serializer.errors, status=status.HTTP_400_BAD_REQUEST)

    def perform_create(self, serializer):
        serializer.save(user=self.request.user)
"""
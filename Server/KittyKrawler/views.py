from KittyKrawler.models import Save, Leaderboard, Item
from KittyKrawler.serializers import SaveSerializer, LeaderboardSerializer, ItemSerializer
from rest_framework.views import APIView
from rest_framework.response import Response
from rest_framework import permissions, status

class SaveView(APIView):
    permission_classes = (permissions.IsAuthenticated,)

    def get(self, request):
        queryset = Save.objects.get(user=request.user)
        serializer = SaveSerializer(queryset)
        return Response(serializer.data)

    def post(self, request):
        serializer = SaveSerializer(data=request.data)
        if serializer.is_valid():
            serializer.save(user=request.user)
            return Response(serializer.data, status=status.HTTP_201_CREATED)
        return Response(serializer.errors, status=status.HTTP_400_BAD_REQUEST)
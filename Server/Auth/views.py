from django.contrib.auth.models import User
from rest_framework import viewsets
from rest_framework import parsers, renderers
from rest_framework.authtoken.models import Token
from rest_framework.authtoken.serializers import AuthTokenSerializer
from rest_framework.response import Response
from rest_framework.views import APIView
from Auth.serializers import UserSerializer

from.permissions import IsStaffOrPOST

class UserViewSet(viewsets.ModelViewSet):
    queryset = User.objects.all()
    serializer_class = UserSerializer
    permission_classes = (IsStaffOrPOST, )


class ObtainAuthToken(viewsets.GenericViewSet):
    serializer_class = AuthTokenSerializer

    def list(self, request):
        return Response(AuthTokenSerializer().data)

    def create(self, request):
        serializer = self.serializer_class(data=request.data)
        serializer.is_valid(raise_exception=True)
        user = serializer.validated_data['user']
        token, created = Token.objects.get_or_create(user=user)
        return Response({'token': token.key})

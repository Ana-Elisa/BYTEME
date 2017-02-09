from django.contrib.auth.models import User
from rest_framework import viewsets, permissions
from Auth.serializers import UserSerializer

from.permissions import IsStaffOrPOST

class UserViewSet(viewsets.ModelViewSet):
    queryset = User.objects.all()
    serializer_class = UserSerializer
    permission_classes = (IsStaffOrPOST, )

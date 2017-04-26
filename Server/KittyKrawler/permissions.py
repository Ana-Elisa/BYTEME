from rest_framework import permissions

TOP_SECRET = "Mcjdan,dryd.ugjtgo.oekrpat!"

class IsAdminOrClient(permissions.BasePermission):
    def has_permission(self, request, view):
        try:
            secret = request.META["HTTP_TOPSECRET"]
        except Exception:
            secret = ""
        return request.user.is_staff or secret == TOP_SECRET

    def has_object_permission(self, request, view, obj):
        try:
            secret = request.META["HTTP_TOPSECRET"]
        except Exception:
            secret = ""
        return request.user.is_staff or secret == TOP_SECRET
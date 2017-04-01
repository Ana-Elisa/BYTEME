# -*- coding: utf-8 -*-
# Generated by Django 1.10.5 on 2017-03-16 02:34
from __future__ import unicode_literals

import annoying.fields
from django.conf import settings
from django.db import migrations
import django.db.models.deletion


class Migration(migrations.Migration):

    dependencies = [
        ('KittyKrawler', '0002_auto_20170316_0041'),
    ]

    operations = [
        migrations.AlterField(
            model_name='save',
            name='user',
            field=annoying.fields.AutoOneToOneField(on_delete=django.db.models.deletion.CASCADE, related_name='save', to=settings.AUTH_USER_MODEL),
        ),
    ]